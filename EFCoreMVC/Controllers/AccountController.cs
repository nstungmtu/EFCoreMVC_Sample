using EFCoreMVC.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Security.Policy;

namespace EFCoreMVC.Controllers
{
    public class AccountController:Controller
    {
        private readonly EFCoreMVCContext _context;
        public AccountController(EFCoreMVCContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        private bool  ValidateLogin(string userName, string password)
        {
            // For this sample, all logins are successful.
            return _context.Users.FirstOrDefault(u=>(u.UserName == userName && u.Password == password))!=null;
        }

        [HttpPost]
        public async Task<IActionResult> Login(string userName, string password, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            // Normally Identity handles sign in, but you can do it directly
            var user = _context.Users.FirstOrDefault(u => (u.UserName == userName && u.Password == password));
            
            if (user != null)
            {
                _context.Roles.Where(r => r.RoleId == user.RoleId).Load();
                var claims = new List<Claim>
                {
                    new Claim("user", userName),
                    new Claim("role", user.Role.RoleName)
                };
                await HttpContext.SignInAsync(new(new ClaimsIdentity(claims, authenticationType: "Cookies", "user", "role")));

                //await HttpContext.SignInAsync(new ClaimsPrincipal(new ClaimsIdentity(claims, "Cookies", "user", "role")));

                if (Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    return Redirect("/");
                }
            }
            ViewData["ErrorMessage"] = "Invalid username or password!";
            return View();
        }

        public IActionResult Denied(string returnUrl = null)
        {
            ViewData["returnUrl"] = returnUrl;
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/");
        }

        [Authorize]
        public IActionResult Info()
        {
            return View();
        }
    }
}
