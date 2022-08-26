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
        public static async Task InitAsync(ApplicationDbContext context, IServiceProvider service)
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
            var adminEmail = "admin@gym,se";

            //Gympass
           var gymClasses = GetGymClasses();
           await db.AddRangeAsync(gymClasses);

            await AddRoleAsync(roleNames);


        }

        private static async Task AddRoleAsync(string[] roleNames)
        {
            
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

