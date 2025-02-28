using BIZBOX.PSA.PERSISTENCE.Context;
using Microsoft.EntityFrameworkCore;

namespace BIZBOX.PSA.API.Configurations
{
    public class Database
    {
        internal static void RegisterEntityFramework(WebApplicationBuilder builder)
        {
            var a = builder.Configuration.GetConnectionString("Default");
            builder.Services.AddDbContext<BPSADbContext>(opts =>
            {
                opts.UseNpgsql(builder.Configuration.GetConnectionString("Default")!, sqlServerOptions => sqlServerOptions.CommandTimeout(60));
            });
        }

        internal static void ConfigureDatabaseMigrations(BPSADbContext context)
        {
            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }

        }

        internal static async Task ConfigureDatabase(IServiceProvider services)
        {
            try
            {
                var context = services.GetRequiredService<BPSADbContext>();

                await context.Database.MigrateAsync();
            }
            catch (Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "An error occurred during migration.");
            }
        }
    }
}
