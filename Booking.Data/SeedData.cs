using Bogus;
using Booking.Core.Entities;
using Booking.Data.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Data
{
    public class SeedData
    {
        private static ApplicationDbContext db = default!;
        private static RoleManager<IdentityRole> roleManager= default!;
        private static UserManager<ApplicationUser> userManager = default!;
        public static async Task InitAsync(ApplicationDbContext context, IServiceProvider service, string adminPW)
        {
            if (context is null ) throw new ArgumentNullException(nameof(context));
            db = context;
            

            if (service is null) throw new ArgumentNullException(nameof(service));
            
            if (db.GymClasses.Any()) return;

            roleManager = service.GetRequiredService<RoleManager<IdentityRole>>();
            if (roleManager is null) throw new ArgumentNullException(nameof(roleManager));

           userManager = service.GetRequiredService<UserManager<ApplicationUser>>();
            if (userManager is null) throw new ArgumentNullException(nameof(userManager));


            var roleNames = new[]{ "Member", "Admin"};
            var adminEmail = "admin@gym.se";

            //Gympass
           var gymClasses = GetGymClasses();
           await db.AddRangeAsync(gymClasses);

            await AddRoleAsync(roleNames);

             var admin = await AddAdminAsync(adminEmail, adminPW);
            await AddToRolesAsync(admin, roleNames);

        }

        private static async Task AddToRolesAsync(ApplicationUser admin, string[] roleNames)
        {

            foreach (var role in roleNames)
            {
                if (await userManager.IsInRoleAsync(admin, role)) continue;
                var result = await userManager.AddToRoleAsync(admin, role);
                if (!result.Succeeded) throw new Exception(string.Join("\n", result.Errors));
            }
          }

        private static async Task <ApplicationUser> AddAdminAsync(string adminEmail, string adminPW)
        {
           // if (await userManager.FindByEmailAsync(adminEmail) != null) return null;

            var admin = new ApplicationUser
            {
                UserName = adminEmail,
                Email = adminEmail
            };
            
           var result = await userManager.CreateAsync(admin, adminPW);
            if (!result.Succeeded) throw new Exception(string.Join("\n", result.Errors));

            return admin;
        }

        private static async Task AddRoleAsync(string[] roleNames)
        {

            foreach(var roleName in roleNames)
            {
                if (await roleManager.RoleExistsAsync(roleName)) continue;
                var role = new IdentityRole { Name = roleName };
                var result = await roleManager.CreateAsync(role);

                if(!result.Succeeded) throw new Exception(string.Join("\n", result.Errors));

            }
            
        }

        private static IEnumerable<GymClass> GetGymClasses()
        {
           var faker = new Faker("sv");

            var gymClasses = new List<GymClass>();

            for(int i = 0; i< 5; i++)
            {
                var temp = new GymClass
                {
                    Name = faker.Company.CatchPhrase(),
                    Description = faker.Hacker.Verb(),
                    Duration = new TimeSpan(0, 55, 0),
                    StartDate = DateTime.Now.AddDays(faker.Random.Int(0, 5))
                };
                gymClasses.Add(temp);
            }
            return gymClasses;
        }
    }
}

