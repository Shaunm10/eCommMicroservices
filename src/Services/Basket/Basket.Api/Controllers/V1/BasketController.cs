using System.Net;
using AutoMapper;
using Basket.Api.Entities.V1;
using Basket.Api.GrpcServices;
using Basket.Api.Repositories;
using EventBus.Messages.Events;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace Basket.Api.Controllers.V1;

[ApiController]
[Route("api/v1/[controller]")]
public class BasketController : ControllerBase
{
    private readonly IBasketRepository _basketRepository;
    private readonly IDiscountService _discountGrpcService;
    private readonly IMapper _mapper;
    private readonly IPublishEndpoint _publishEndpoint;

    public BasketController(IBasketRepository basketRepository, IDiscountService discountGrpcService, IMapper mapper, IPublishEndpoint publishEndpoint)
    {
        this._discountGrpcService = discountGrpcService ?? throw new ArgumentNullException(nameof(discountGrpcService));
        this._mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        this._publishEndpoint = publishEndpoint ?? throw new ArgumentNullException(nameof(publishEndpoint));
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

    [HttpDelete("{userName}", Name = "DeleteBCasket")]
    [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> DeleteBasket(string userName)
    {
        await this._basketRepository.DeleteBasket(userName);
        return this.Ok();
    }

    [HttpPost]
    [Route("[action]")]
    [ProducesResponseType((int)HttpStatusCode.Accepted)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Checkout([FromBody] BasketCheckout basketCheckout)
    {
        // verify the request has a user name
        if (basketCheckout?.UserName == null)
        {
            return this.BadRequest($"{nameof(basketCheckout.UserName)} must be supplied");
        }

        // get existing basket with total price
        var basket = await this._basketRepository.GetBasket(basketCheckout.UserName);

        if (basket == null)
        {
            return this.BadRequest($"Unable to locate basket for user {basketCheckout.UserName}");
        }

        // set totalPrice on basketCheckout eventMessage
        var eventMessage = this._mapper.Map<BasketCheckoutEvent>(basketCheckout);
        eventMessage.TotalPrice = basket.TotalPrice;

        // send checkout event to rabbitmq
        await this._publishEndpoint.Publish(eventMessage);

        // remove the basket from repository.
        await this._basketRepository.DeleteBasket(basketCheckout.UserName);

        return this.Accepted();
    }
}
