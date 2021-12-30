using System.Text.Json;
using Basket.Api.Entities.V1;
using Microsoft.Extensions.Caching.Distributed;

namespace Basket.Api.Repositories;

public class BasketRepository : IBasketRepository
{
    private readonly IDistributedCache _redisCache;
    private const string BasketCachePrefix = nameof(BasketRepository);
    private const string VersionNumber = "1.0.0.0";

    public BasketRepository(IDistributedCache distributedCache)
    {
        this._redisCache = distributedCache ?? throw new ArgumentNullException(nameof(distributedCache));
    }

    public async Task DeleteBasket(string userName)
    {
        var key = GetCacheKey(userName);
        await this._redisCache.RemoveAsync(key);
    }

    public async Task<ShoppingCart?> GetBasket(string userName)
    {
        var key = GetCacheKey(userName);
        var basketJson = await this._redisCache.GetStringAsync(key);

        if (string.IsNullOrWhiteSpace(basketJson))
        {
            return null;
        }

        var cart = JsonSerializer.Deserialize<ShoppingCart>(basketJson);

        return cart;
    }

    public async Task<ShoppingCart?> UpdateBasket(ShoppingCart basket)
    {
        if (!string.IsNullOrWhiteSpace(basket.UserName))
        {
            var key = GetCacheKey(basket.UserName);
            var cartJson = JsonSerializer.Serialize(basket);

            await this._redisCache.SetStringAsync(key, cartJson);

            return await this.GetBasket(basket!.UserName);
        }

        return null;
    }

    private static string GetCacheKey(string userName)
    {
        var key = $"{BasketCachePrefix}.{VersionNumber}.{userName}";
        return key;
    }
}
