using Shopping.Aggregator.Extensions;
using Shopping.Aggregator.Models;

namespace Shopping.Aggregator.ProxyServices;

public class BasketService : IBasketService
{
    private readonly HttpClient _client;

    public BasketService(HttpClient client)
    {
        this._client = client ?? throw new ArgumentNullException(nameof(client));
    }

    public async Task<BasketModel?> GetBasketAsync(string userName)
    {
        var response = await this._client.GetAsync($"/api/v1/Basket/{userName}");

        return await response.ReadContentAs<BasketModel>() ?? null;
    }
}
