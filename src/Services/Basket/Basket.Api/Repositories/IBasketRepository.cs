using Basket.Api.Entities.V1;

namespace Basket.Api.Repositories;

public interface IBasketRepository
{
    Task<ShoppingCart?> GetBasket(string userName);

    Task<ShoppingCart?> UpdateBasket(ShoppingCart basket);

    Task DeleteBasket(string userName);
}
