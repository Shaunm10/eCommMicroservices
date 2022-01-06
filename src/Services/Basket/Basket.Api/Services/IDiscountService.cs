using Discount.Grpc.Protos;

namespace Basket.Api.GrpcServices
{
    public interface IDiscountService
    {
        public Task<DiscountModel> GetDiscountAsync(string productId);

        public Task<IEnumerable<DiscountModel>> GetDiscountsAsync(IEnumerable<string> productIds);
    }
}
