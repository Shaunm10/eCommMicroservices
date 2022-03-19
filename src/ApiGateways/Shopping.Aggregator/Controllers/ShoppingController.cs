using System.Net;
using Microsoft.AspNetCore.Mvc;
using Shopping.Aggregator.Models;
using Shopping.Aggregator.Services;

namespace Shopping.Aggregator.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class ShoppingController : ControllerBase
{
    private readonly ICatalogService catalogService;
    private readonly IBasketService basketService;
    private readonly IOrderService orderService;

    public ShoppingController(ICatalogService catalogService, IBasketService basketService, IOrderService orderService)
    {
        this.catalogService = catalogService ?? throw new ArgumentNullException(nameof(catalogService));
        this.basketService = basketService ?? throw new ArgumentNullException(nameof(basketService));
        this.orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));
    }

    [HttpGet("{userName}", Name = "GetShopping")]
    [ProducesResponseType(typeof(ShoppingModel), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ShoppingModel>> GetShopping(string userName)
    {
        // get basket from userName
        var basket = await this.basketService.GetBasketAsync(userName);

        // if there is no basket, then return short.
        if (basket is null)
        {
            return new ShoppingModel
            {
                UserName = userName
            };
        }

        // iterate basket items and consume products with basket item productId members
        foreach (var item in basket.Items)
        {
            var product = await this.catalogService.GetCatalogAsync(item.ProductId);

            // map product related members nto basketitem dto with extend column
            item.ProductName = product?.Name;
            item.Category = product?.Category;
            item.Summary = product?.Summary;
            item.Description = product?.Description;
            item.ImageFile = product?.ImageFile;
        }

        var orders = await this.orderService.GetOrderByUserNameAsync(userName);

        var shoppingModel = new ShoppingModel
        {
            Orders = orders,
            BasketWithProducts = basket,
            UserName = userName
        };

        return this.Ok(shoppingModel);
    }
}