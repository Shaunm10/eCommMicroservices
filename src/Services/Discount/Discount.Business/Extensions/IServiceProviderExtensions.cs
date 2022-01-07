using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using Npgsql;

namespace Discount.Business.Extensions
{
    public static class IServiceProviderExtensions
    {
        public static IServiceProvider MigrateDatabase<TContext>(this IServiceProvider serviceProvider, string databaseConnectionString, int retry = 0)
        {
            int retryForAvailability = retry;

            var logger = serviceProvider.GetService<ILogger<TContext>>();

            if (string.IsNullOrWhiteSpace(databaseConnectionString))
            {
                logger?.LogWarning($"{nameof(databaseConnectionString)} is Null or whitespace.");
            }

            try
            {
                logger!.LogInformation("Attempting to migrating postgres database...");
                using var connection = new NpgsqlConnection(databaseConnectionString);
                connection.Open();

                using var command = new NpgsqlCommand
                {
                    Connection = connection
                };

                command.CommandText = "DROP TABLE IF EXISTS Discount";
                command.ExecuteNonQuery();

                command.CommandText = @"CREATE Table Discount(
	                                        ID SERIAL PRIMARY KEY NOT NULL,
	                                        ProductID VARCHAR(24) NOT NULL,
	                                        Description TEXT,
	                                        AMOUNT INT
                                        );";

                command.ExecuteNonQuery();

                command.CommandText = "INSERT INTO Discount (productId, description, amount) Values ('602d2149e773f2a3990b47f5','IPhone Discount',150);";
                command.ExecuteNonQuery();

                command.CommandText = "INSERT INTO Discount (productId, description, amount) Values ('602d2149e773f2a3990b47f6','Samsung 10 Discount',100);";
                command.ExecuteNonQuery();
                logger?.LogInformation("Migrated postgres database successful.");
            }
            catch (NpgsqlException ex)
            {
                logger?.LogError(ex, "An error occurred while migrating the postgresql database");

                if (retryForAvailability < 50)
                {
                    retryForAvailability++;

                    // wait for 2 seconds
                    Thread.Sleep(2000);

                    logger?.LogWarning($"Retrying Migration on attempt number {retryForAvailability}");

                    // call the method again.
                    MigrateDatabase<TContext>(serviceProvider, databaseConnectionString, retryForAvailability);
                }
            }

            return serviceProvider;
        }
    }
}
