using Discount.Grpc.Protos;
using Grpc.Core;

namespace Basket.Api.GrpcServices;

public class DiscountService : IDiscountService
{
    private readonly DiscountProtoService.DiscountProtoServiceClient discountProtoService;

    public DiscountService(DiscountProtoService.DiscountProtoServiceClient discountProtoService)
    {
        this.discountProtoService = discountProtoService ?? throw new ArgumentNullException(nameof(discountProtoService));
    }

    public async Task<DiscountModel> GetDiscountAsync(string productId)
    {
        var discount = await this.discountProtoService.GetDiscountAsync(new GetDiscountRequest
        {
            ProductId = productId
        });

        return discount;
    }

    public async Task<IEnumerable<DiscountModel>> GetDiscountsAsync(IEnumerable<string> productIds)
    {
        var discountTasks = new List<Task<DiscountModel>>();

        productIds.ToList().ForEach(x =>
        {
            var productDiscount = this.discountProtoService.GetDiscountAsync(new GetDiscountRequest { ProductId = x });
            discountTasks.Add(productDiscount.ResponseAsync);
        });

        await Task.WhenAll(discountTasks);
        var discounts = discountTasks.Select(x => x.Result);

        return discounts;
    }
}