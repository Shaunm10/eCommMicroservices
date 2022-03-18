using System;
using Shopping.Aggregator.Extensions;
using Shopping.Aggregator.Models;

namespace Shopping.Aggregator.Services;

public class CatalogService : ICatalogService
{
    private readonly HttpClient _client;

    public CatalogService(HttpClient client)
    {
        this._client = client ?? throw new ArgumentNullException(nameof(client));
    }

    public async Task<IEnumerable<CatalogModel>> GetCatalog()
    {
        var response = await this._client.GetAsync("/api/v1/Catalog");

        return await response.ReadContentAs<List<CatalogModel>>() ?? new List<CatalogModel>();
    }

    public async Task<CatalogModel?> GetCatalog(string id)
    {
        var response = await this._client.GetAsync($"/api/v1/Catalog/{id}");
        return await response.ReadContentAs<CatalogModel?>() ?? null;
    }

    public async Task<IEnumerable<CatalogModel>> GetCatalogByCategory(string categoryName)
    {
        var response = await this._client.GetAsync($"/api/v1/Catalog/{categoryName}");
        return await response.ReadContentAs<List<CatalogModel>>() ?? new List<CatalogModel>();
    }
}
