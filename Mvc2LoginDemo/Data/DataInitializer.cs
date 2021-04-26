using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Mvc2LoginDemo.Data
{
    public class DataInitializer
    {
        public static void SeedData(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            context.Database.Migrate();
            AddRoleIfNotExists(context, "Admin");
            AddRoleIfNotExists(context, "Customer");
            AddUserIfNotExists(userManager, "admin@hejhopp.nl", "Hejsan123#", new string[] { "Admin" });
            AddUserIfNotExists(userManager, "customer@hejhopp.nl", "Hejsan123#", new string[] { "Customer" });
            AddUserIfNotExists(userManager, "stefan@hejhopp.nl", "Hejsan123#", new string[] { "Customer", "Admin" });

            //SeedUses(context,"" );
        }

        private static void AddRoleIfNotExists(ApplicationDbContext context, string role)
        {
            if (context.Roles.Any(r => r.Name == role)) return;
            context.Roles.Add(new IdentityRole { Name = role, NormalizedName = role });
            context.SaveChanges();
        }

        private static void AddUserIfNotExists(UserManager<IdentityUser> userManager,
            string userName, string password, string[] roles)
        {
            if (userManager.FindByEmailAsync(userName).Result != null) return;

            var user = new IdentityUser
            {
                UserName = userName,
                Email = userName,
                EmailConfirmed = true
            };
            var result = userManager.CreateAsync(user, password).Result;
            var r = userManager.AddToRolesAsync(user, roles).Result;
        }



    }
}