using Booking.Data;
using Booking.Data.Data;
using Microsoft.EntityFrameworkCore;

namespace Booking.Web.Extentions
{
    public static class ApplicationBuilderExtentions
    {

        public static async Task<ApplicationBuilder> SeedDataAsync(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;
                var _context = serviceProvider.GetRequiredService<ApplicationDbContext>();

                //_context.Database.EnsureCreated();
                //_context.Database.Migrate();

                try
                {
                    await SeedData.InitAsync(db, serviceProvider);
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
