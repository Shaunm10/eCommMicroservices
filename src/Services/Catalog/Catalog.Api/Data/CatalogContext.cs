using Catalog.Api.Entities;
using MongoDB.Driver;

namespace Catalog.Api.Data;

public class CatalogContext : ICatalogContext
{
    public CatalogContext(IConfiguration configuration)
    {
        var client = new MongoClient(configuration.GetValue<string>("DatabasSettings:ConnectionString"));
    }
    public IMongoCollection<Product> Products => throw new NotImplementedException();
}
