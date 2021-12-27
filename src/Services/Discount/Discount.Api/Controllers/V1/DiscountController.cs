using Discount.Api.Entities.V1;
using Discount.Api.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Discount.Api.Controllers.V1;

[Route("api/v1/[controller]")]
[ApiController]
public class DiscountController : ControllerBase
{
    private readonly IDiscountRepository discountRepository;
    private readonly ILogger<DiscountController> logger;

    public DiscountController(IDiscountRepository discountRepository, ILogger<DiscountController> logger)
    {
        this.discountRepository = discountRepository ?? throw new ArgumentException(nameof(discountRepository));
        this.logger = logger ?? throw new ArgumentException(nameof(logger));
    }

    [HttpGet("[action]/{productId}", Name = "GetDiscount")]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(Entities.V1.Discount), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<Entities.V1.Discount>> GetDiscount(string productId)
    {
        var discount = await this.discountRepository.GetDiscountAsync(productId);

        if (discount == null)
        {
            this.logger.LogWarning($"No discount found for ProductId: {productId}");
            return this.NotFound();
        }

        return this.Ok(discount);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Entities.V1.Discount), (int)HttpStatusCode.Created)]
    public async Task<ActionResult<Entities.V1.Discount>> CreateDiscount([FromBody] Entities.V1.Discount coupon)
    {
        await this.discountRepository.CreateDiscountAsync(coupon);
        return this.CreatedAtRoute("GetDiscount", new { productId = coupon.ProductId }, coupon);
    }

    [HttpPut]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(Entities.V1.Discount), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<Entities.V1.Discount>> UpdateDiscount([FromBody] Entities.V1.Discount coupon)
    {
        var successful = await this.discountRepository.UpdateDiscountAsync(coupon);

        if (successful)
        {
            return this.Ok();
        }

        return this.NotFound();
    }

    [HttpDelete]
    [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<bool>> DeleteDiscount(string productId)
    {
        return this.Ok(await this.discountRepository.DeleteDiscountAsync(productId));
    }
}