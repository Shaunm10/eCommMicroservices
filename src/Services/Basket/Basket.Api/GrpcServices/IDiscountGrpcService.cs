using Discount.Grpc.Protos;

namespace Basket.Api.GrpcServices
{
    public interface IDiscountGrpcService
    {
        public Task<DiscountModel> GetDiscountAsync(string productId);
    }
}
