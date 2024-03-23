using Microsoft.AspNetCore.Authorization;
using System.Security.Cryptography.X509Certificates;

namespace EFCoreMVC.Authorization
{
    public class RoleAuthorizationHandler : AuthorizationHandler<RoleAuthorizationAttribute>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, RoleAuthorizationAttribute requirement)
        {
            var roleClaim = context.User.Claims.FirstOrDefault(x => x.Type == "role");
            if (roleClaim != null)
            {
                string role = roleClaim.Value;
                if(requirement.Roles.Contains(role)) context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
    public class RoleAuthorizationAttribute : AuthorizeAttribute, IAuthorizationRequirement, IAuthorizationRequirementData
    {
        public string Roles { get; set; }
        public RoleAuthorizationAttribute(string roles) => Roles = roles;
        public IEnumerable<IAuthorizationRequirement> GetRequirements()
        {
            yield return this;
        }
    }
}
