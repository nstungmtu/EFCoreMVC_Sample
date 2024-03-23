using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace EFCoreMVC.Data
{
    public class EFCoreMVCContext:DbContext
    {
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        /*public DbSet<RoleUser> UserRoles { get; set; } */

        public EFCoreMVCContext(DbContextOptions<EFCoreMVCContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            /*modelBuilder.Entity<Role>()
                .HasMany(e => e.Users)
                .WithMany(e => e.Roles)
                .UsingEntity<RoleUser>();*/
            //modelBuilder.Entity<RoleUser>().HasNoKey();
            /*modelBuilder.Entity<Role>().ToTable(nameof(Role)+"s");
            modelBuilder.Entity<User>().ToTable(nameof(User)+"s");
            modelBuilder.Entity<UserRole>().ToTable(nameof(UserRole) +"s");*/
        }
    }

    [DisplayColumn("RoleName")]
    public class Role
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public string Description { get; set; }
        public List<User> Users { get; } = [];
    }

    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
    }
    /*public class RoleUser
    {
        public int RoleId { get; set; }
        public int UserId { get; set; }
        public Role Role { get; set; } = null!;
        public User User { get; set; } = null!;
    }*/
}
