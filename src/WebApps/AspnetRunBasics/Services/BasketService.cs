using AspnetRunBasics.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace AspnetRunBasics.Services
{
    public class BasketService : IBasketService
    {
        private readonly HttpClient _client;

        public BasketService(HttpClient client)
        {
            this._client = client ?? throw new ArgumentNullException(nameof(client));
        }
        public Task CheckoutBasketAsync(BasketCheckoutModel model)
        {
            throw new System.NotImplementedException();
        }

        public Task<BasketModel> GetBasketAsync(string userName)
        {
            throw new System.NotImplementedException();
        }

        public Task<BasketModel> UpdateBasketAsync(BasketModel model)
        {
            throw new System.NotImplementedException();
        }
    }
}
