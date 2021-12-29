using Discount.Common.Entities.V1;

namespace Discount.Grpc.Repositories;

public interface IDiscountRepository
{
    Task<Discount.Common.Entities.V1.Discount?> GetDiscountAsync(string productId);

    Task<bool> CreateDiscountAsync(Discount.Common.Entities.V1.Discount coupon);

    Task<bool> UpdateDiscountAsync(Discount.Common.Entities.V1.Discount coupon);

    Task<bool> DeleteDiscountAsync(string productId);
}