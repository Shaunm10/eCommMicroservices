using Npgsql;
using System.Linq;

namespace Discount.Api.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static WebApplicationBuilder MigrateDatabase<TContext>(this WebApplicationBuilder builder, int? retry = 0)
        {
            int retryForAvailabiliyt = retry.Value;

            var services = builder.Build().Services;
            var configuration = services.GetService<IConfiguration>();
            var logger = services.GetService<ILogger<TContext>>();

            try 
            {
                logger.LogInformation("Migrating postgres database.");
                using var connection = new NpgsqlConnection(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
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

                command.CommandText = "INSERT INTO Discount (productid, description, amount) Values ('602d2149e773f2a3990b47f5','IPhone Discount',150);";
                command.ExecuteNonQuery();

                command.CommandText = "INSERT INTO Discount (productid, description, amount) Values ('602d2149e773f2a3990b47f6','Samsung 10 Discount',100);";
                command.ExecuteNonQuery();
            }
            catch (Exception) 
            {
                throw;
            }

            return builder;
        }

        
    }
}
