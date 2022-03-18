using Microsoft.AspNetCore.Mvc;
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
}