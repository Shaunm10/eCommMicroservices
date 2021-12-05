using Catalog.Api.Entities;
using MongoDB.Driver;

namespace Catalog.Api.Data;

public class CatalogContext : ICatalogContext
{
    public CatalogContext(IConfiguration configuration)
    {
        var client = new MongoClient(configuration.GetValue<string>("DatabasSettings:ConnectionString"));
        var database = client.GetDatabase(configuration.GetValue<string>("DatabasSettings:DatabaseName"));

        this.Products = database.GetCollection<Product>(configuration.GetValue<string>("DatabasSettings:CollectionName"));

        // seed data
        CatalogContextSeed.SeedData(this.Products);

    }
    public IMongoCollection<Product> Products { get; }
}
