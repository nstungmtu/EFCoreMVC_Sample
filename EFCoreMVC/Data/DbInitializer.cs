namespace EFCoreMVC.Data
{
    public static class DbInitializer
    {
        public static void Initialize(EFCoreMVCContext context)
        {
            //context.Database.EnsureCreated();
            // Look for any students.
            if (context.Roles.Any())
            {
                return;   // DB has been seeded
            }
            //context.Database.EnsureDeleted();
            //context.Database.EnsureCreated();
            var roles = new Role[]
            {
                new Role() {RoleId=1,RoleName="Administrator",Description="Nhóm quản trị hệ thống"},
                new Role() {RoleId=2,RoleName="Manager",Description="Nhóm quản lý"},
                new Role() {RoleId=3,RoleName="Team Leader",Description="Nhóm đội trưởng"},
                new Role() {RoleId=4,RoleName="Employee",Description="Nhóm nhân viên"}
            };

            context.Roles.AddRange(roles);
            context.SaveChanges();


            var users = new User[]
            {
                new User(){UserId=1,UserName="admin",Firstname="Cassady",Lastname="Hess",RoleId=1,Password="123456"},
                new User(){UserId=2,UserName="manager",Firstname="Dorothy",Lastname="Bonner",RoleId=2,Password="123456"},
                new User(){UserId=3,UserName="teamleader",Firstname="Courtney",Lastname="Medina", RoleId = 3, Password = "123456"},
                new User(){UserId=4,UserName="employee1",Firstname="Omar",Lastname="Bowman",RoleId=4, Password = "123456"},
                new User(){UserId=5,UserName="employee2",Firstname="Yeo",Lastname="Haynes", RoleId = 4, Password="123456"}
            };

            context.Users.AddRange(users);
            context.SaveChanges();
            

            /*var user_role = new RoleUser[]
            {
                new RoleUser() {RoleId=1,UserId=1},
                new RoleUser() {RoleId=2,UserId=2},
                new RoleUser() {RoleId=3,UserId=3},
                new RoleUser() {RoleId=4,UserId=3},
                new RoleUser() {RoleId=4,UserId=4},
                new RoleUser() {RoleId=3,UserId=4},
                new RoleUser() {RoleId=4,UserId=5}
            };

            context.UserRoles.AddRange(user_role);
            context.SaveChanges();*/
        }
    }
}
