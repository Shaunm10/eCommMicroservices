using System;
using Shopping.Aggregator.Extensions;
using Shopping.Aggregator.Models;

namespace Shopping.Aggregator.ProxyServices;

public class OrderService : IOrderService
{
    private readonly HttpClient _client;

    public OrderService(HttpClient client)
    {
        this._client = client ?? throw new ArgumentNullException(nameof(client));
    }

    public async Task<IEnumerable<OrderResponseModel>> GetOrderByUserNameAsync(string userName)
    {
        var response = await this._client.GetAsync($"/api/v1/Order/{userName}");
        return await response.ReadContentAs<List<OrderResponseModel>>() ?? new List<OrderResponseModel>();
    }
}
