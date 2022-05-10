using AspnetRunBasics.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace AspnetRunBasics.Services
{
    public class OrderService : IOrderService
    {
        private readonly HttpClient _client;

        public OrderService(HttpClient client)
        {
            this._client = client ?? throw new ArgumentNullException(nameof(client));
        }
        public Task<IEnumerable<OrderResponseModel>> GetOrderByUserNameAsync(string userName)
        {
            throw new System.NotImplementedException();
        }
    }
}
