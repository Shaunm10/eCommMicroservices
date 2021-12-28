namespace Discount.Api.Extensions
{
    public static class HostExtensions
    {
        public static ConfigureHostBuilder MigrateDatabase<TContext>(this ConfigureHostBuilder host, int? retry = 0)
        {
            return host;
        }
    }
}
