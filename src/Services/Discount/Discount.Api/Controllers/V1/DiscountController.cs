using Microsoft.AspNetCore.Mvc;
using System.Net;
using Discount.Business.Repositories;

namespace Discount.Api.Controllers.V1;

[Route("api/v1/[controller]")]
[ApiController]
public class DiscountController : ControllerBase
{
    private readonly IDiscountRepository discountRepository;
    private readonly ILogger<DiscountController> logger;

    public DiscountController(IDiscountRepository discountRepository, ILogger<DiscountController> logger)
    {
        this.discountRepository = discountRepository ?? throw new ArgumentException(null, nameof(discountRepository));
        this.logger = logger ?? throw new ArgumentException(null, nameof(logger));
    }

    [HttpGet("[action]/{productId}", Name = "GetDiscount")]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(Business.Entities.V1.Discount), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<Business.Entities.V1.Discount>> GetDiscount(string productId)
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
    [ProducesResponseType(typeof(Business.Entities.V1.Discount), (int)HttpStatusCode.Created)]
    public async Task<ActionResult<Business.Entities.V1.Discount>> CreateDiscount([FromBody] Business.Entities.V1.Discount discount)
    {
        await this.discountRepository.CreateDiscountAsync(discount);
        return this.CreatedAtRoute("GetDiscount", new { productId = discount.ProductId }, discount);
    }

    [HttpPut]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(Business.Entities.V1.Discount), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<Business.Entities.V1.Discount>> UpdateDiscount([FromBody] Business.Entities.V1.Discount discount)
    {
        var successful = await this.discountRepository.UpdateDiscountAsync(discount);

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