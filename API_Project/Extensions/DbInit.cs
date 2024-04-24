using Core.Models.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Repository.Context;
using Repository.DataSeeding;

namespace API_Project.Extensions
{
    public static class DbInit
    {
        public static async Task InitializeDbAsync(WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var service = scope.ServiceProvider;
                var loggerFactory = service.GetRequiredService<ILoggerFactory>();
                var manager = service.GetRequiredService<UserManager<ApplicationUser>>();
                try
                {
                    var context = service.GetRequiredService<ApiProjectContext>();
                    if ((await context.Database.GetPendingMigrationsAsync()).Any())
                        await context.Database.MigrateAsync();
                    await DataSeed.SeedData(context);
                    await UsersSeed.SeedUsers(manager);
                }
                catch (Exception ex)
                {
                    var logger = loggerFactory.CreateLogger<Program>();
                    logger.LogError(ex.Message);
                }
            }

        }

    }
}
