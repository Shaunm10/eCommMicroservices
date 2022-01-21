using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Ordering.Api.Extensions
{
    public static class WebApplicationExtensions
    {
        public static WebApplication MigrateDatabase<TContext>(
            this WebApplication webApplication,
            Action<TContext, IServiceProvider> seeder,
            int? retry = 0)
            where TContext : DbContext
        {
            int retryForAvailability = retry.GetValueOrDefault();

            using (var scope = webApplication.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var logger = services.GetRequiredService<ILogger<TContext>>();
                var context = services.GetService<TContext>();

                try
                {
                    logger.LogInformation("Migrating database associated with context {DbContextName}", typeof(TContext).Name);

                    InvokeSeeder(seeder!, context, services);

                    logger.LogInformation("Migrated database associated with context {DbContextName}", typeof(TContext).Name);
                }
                catch (SqlException sqlEx)
                {
                    logger.LogError(sqlEx, "Error Migrating database with context {DbContextName}", typeof(TContext).Name);

                    if (retryForAvailability < 50)
                    {
                        retryForAvailability++;
                        Thread.Sleep(2000);

                        // recursively call it again.
                        MigrateDatabase<TContext>(webApplication, seeder, retryForAvailability);
                    }
                }
            }

            return webApplication;
        }

        private static void InvokeSeeder<TContext>(
            Action<TContext, IServiceProvider> seeder,
            TContext context,
            IServiceProvider services)
                where TContext : DbContext
        {
            context.Database.Migrate();
            seeder(context, services);
        }
    }
}
