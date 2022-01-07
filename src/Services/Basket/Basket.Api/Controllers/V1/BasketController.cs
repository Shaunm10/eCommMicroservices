using System.Net;
using Basket.Api.Entities.V1;
using Basket.Api.GrpcServices;
using Basket.Api.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Basket.Api.Controllers.V1;

[ApiController]
[Route("api/v1/[controller]")]
public class BasketController : ControllerBase
{
    private readonly IBasketRepository _basketRepository;
    private readonly IDiscountService _discountGrpcService;

    public BasketController(IBasketRepository basketRepository, IDiscountService discountGrpcService)
    {
        this._discountGrpcService = discountGrpcService ?? throw new ArgumentNullException(nameof(discountGrpcService));
        this._basketRepository = basketRepository ?? throw new ArgumentNullException(nameof(basketRepository));
    }

    [HttpGet("{userName}", Name = "GetBasket")]
    [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ShoppingCart>> GetBasket(string userName)
    {
        var basket = await this._basketRepository.GetBasket(userName);
        return this.Ok(basket ?? new ShoppingCart(userName));
    }

    [HttpPost]
    [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ShoppingCart>> UpdateBasket([FromBody] ShoppingCart basket)
    {
        var productIds = basket.Items
                .Select(x => x.ProductId)
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Distinct();

        var discounts = await this._discountGrpcService.GetDiscountsAsync(productIds!);

        basket.Items.ForEach(item =>
        {
            var discountForItem = discounts.Discounts.FirstOrDefault(x => x.ProductId == item.ProductId);
            if (discountForItem != null)
            {
                // adjust the price with the discount
                item.Price -= (decimal)discountForItem.Amount;
            }
        });

        var newBasket = await this._basketRepository.UpdateBasket(basket);
        return this.Ok(newBasket);
    }

    [HttpDelete("{userName}", Name = "DeleteBasket")]
    [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> DeleteBasket(string userName)
    {
        await this._basketRepository.DeleteBasket(userName);
        return this.Ok();
    }
}
