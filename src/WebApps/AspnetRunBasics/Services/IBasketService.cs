using AspnetRunBasics.Models;
using System.Threading.Tasks;

namespace AspnetRunBasics.Services;

public interface IBasketService
{
    Task<BasketModel> GetBasketAsync(string userName);
    Task<BasketModel> UpdateBasketAsync(BasketModel model);
    Task CheckoutBasketAsync(BasketCheckoutModel model);
}
