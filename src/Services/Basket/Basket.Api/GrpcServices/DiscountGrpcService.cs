using Discount.Grpc.Protos;

namespace Basket.Api.GrpcServices;

public class DiscountGrpcService
{
    private readonly DiscountProtoService.DiscountProtoServiceClient discountProtoService;

    public DiscountGrpcService(DiscountProtoService.DiscountProtoServiceClient discountProtoService)
    {
        this.discountProtoService = discountProtoService;
    }

    public async Task<DiscountModel> GetDiscountAsync(string productId)
    {
        var discount = await this.discountProtoService.GetDiscountAsync(new GetDiscountRequest
        {
            ProductId = productId
        });

        return discount;
    }
}

