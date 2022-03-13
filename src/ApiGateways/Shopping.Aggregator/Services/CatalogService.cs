using System;
using Shopping.Aggregator.Models;

namespace Shopping.Aggregator.Services;

public class CatalogService : ICatalogService
{
    private readonly HttpClient _http;

    public CatalogService(HttpClient http)
    {
        this._http = http;
    }

    public Task<IEnumerable<CatalogModel>> GetCatalog()
    {
        throw new NotImplementedException();
    }

    public Task<CatalogModel> GetCatalog(string id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<CatalogModel>> GetCatalogByCategory(string categoryName)
    {
        throw new NotImplementedException();
    }
}
