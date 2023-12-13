using CodePracticeTrackingApp.Data.StaticData;
using CodePracticeTrackingApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CodePracticeTrackingApp.Data.DBInitializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly DatabaseContext _db;

        public DbInitializer(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, DatabaseContext db)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _db = db;
        }

        public void Initialize()
        {
            // migration if they are not applied
            try
            {
                if (_db.Database.GetPendingMigrations().Count() > 0)
                {
                    _db.Database.Migrate();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }


            // create roles if they are not created
            if (!_roleManager.RoleExistsAsync(Constant.Role_SWE).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(Constant.Role_SWE)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(Constant.Role_Manager)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(Constant.Role_Student)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(Constant.Role_Dev)).GetAwaiter().GetResult();

                // if roles are not created, then we wil create admin user as well
                _userManager.CreateAsync(new ApplicationUser
                {
                    UserName = "demo@gmail.com",
                    Email = "demo@gmail.com",
                    Name = "Demo user",
                }, "@Demo123").GetAwaiter().GetResult();


                ApplicationUser user = _db.ApplicationUsers.FirstOrDefault(u => u.UserName == "demo@gmail.com");
                _userManager.AddToRoleAsync(user, Constant.Role_SWE).GetAwaiter().GetResult();
            }

            return;
        }
    }
}
