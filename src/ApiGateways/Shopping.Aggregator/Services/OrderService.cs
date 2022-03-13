using System;
using Shopping.Aggregator.Models;

namespace Shopping.Aggregator.Services;

public class OrderService : IOrderService
{
    private readonly HttpClient _client;

    public OrderService(HttpClient client)
    {
        this._client = client ?? throw new ArgumentNullException(nameof(client));
    }

    public Task<IEnumerable<OrderResponseModel>> GetOrderByUserName(string userName)
    {
        throw new NotImplementedException();
    }
}
