using System;
using Shopping.Aggregator.Models;

namespace Shopping.Aggregator.Services;

public class OrderService : IOrderService
{
    private readonly HttpClient _http;

    public OrderService(HttpClient http)
    {
        this._http = http;
    }

    public Task<IEnumerable<OrderResponseModel>> GetOrderByUserName(string userName)
    {
        throw new NotImplementedException();
    }
}
