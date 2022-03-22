using System;
using Shopping.Aggregator.Models;

namespace Shopping.Aggregator.ProxyServices;

public interface IOrderService
{
    Task<IEnumerable<OrderResponseModel>> GetOrderByUserNameAsync(string userName);
}
