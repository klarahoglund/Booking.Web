using Booking.Data;
using Booking.Data.Data;
using Microsoft.EntityFrameworkCore;

namespace Booking.Web.Extentions
{
    public static class ApplicationBuilderExtentions
    {

        public static async Task<IApplicationBuilder> SeedDataAsync(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;
                var _context = serviceProvider.GetRequiredService<ApplicationDbContext>();

                //_context.Database.EnsureCreated();
                //_context.Database.Migrate();
                var config= serviceProvider.GetRequiredService<IConfiguration>();
                var adminPW = config["AdminPW"];

                try
                {
                    await SeedData.InitAsync(_context, serviceProvider, adminPW);
                }
                catch (Exception e)
                {
                    throw;
                }
            }
            return app;

        }
    }
}
