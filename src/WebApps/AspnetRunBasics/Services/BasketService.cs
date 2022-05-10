using AspnetRunBasics.Models;
using System.Threading.Tasks;

namespace AspnetRunBasics.Services
{
    public class BasketService : IBasketService
    {
        public BasketService()
        {

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
