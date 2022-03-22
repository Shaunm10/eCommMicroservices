using System;
using Shopping.Aggregator.Models;

namespace Shopping.Aggregator.ProxyServices;

public interface IBasketService
{
    Task<BasketModel?> GetBasketAsync(string userName);
}
