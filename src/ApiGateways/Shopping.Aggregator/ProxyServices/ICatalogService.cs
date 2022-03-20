using Shopping.Aggregator.Models;

namespace Shopping.Aggregator.ProxyServices;

public interface ICatalogService
{
    Task<IEnumerable<CatalogModel>> GetCatalog();

    Task<IEnumerable<CatalogModel>> GetCatalogByCategory(string categoryName);

    Task<CatalogModel?> GetCatalogAsync(string id);
}
