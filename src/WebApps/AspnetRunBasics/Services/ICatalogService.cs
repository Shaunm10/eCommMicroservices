using AspnetRunBasics.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspnetRunBasics.Services;

public interface ICatalogService
{
    Task<IEnumerable<CatalogModel>> GetCatalogAsync();
    Task<IEnumerable<CatalogModel>> GetCatalogByCategory(string category);
    Task<CatalogModel> GetCatalogAsync(string id);
    Task<CatalogModel> CreateCatalogAsync(CatalogModel model);
}
