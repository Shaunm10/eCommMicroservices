using System;
using Shopping.Aggregator.Models;

namespace Shopping.Aggregator.Services;

public class BasketService : IBasketService
{
    public BasketService()
    {
    }

    public Task<BasketModel> GetBasket(string userName)
    {
        throw new NotImplementedException();
    }
}
