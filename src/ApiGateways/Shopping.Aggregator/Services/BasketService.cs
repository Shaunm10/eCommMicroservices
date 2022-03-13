using System;
using Shopping.Aggregator.Models;

namespace Shopping.Aggregator.Services;

public class BasketService : IBasketService
{
    private readonly HttpClient _http;

    public BasketService(HttpClient http)
    {
        this._http = http;
    }

    public Task<BasketModel> GetBasket(string userName)
    {
        throw new NotImplementedException();
    }
}
