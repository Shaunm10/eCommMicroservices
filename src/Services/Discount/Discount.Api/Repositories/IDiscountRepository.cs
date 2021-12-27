using Discount.Api.Entities.V1;

namespace Discount.Api.Repositories;

public interface IDiscountRepository
{
    Task<Coupon?> GetDiscountAsync(string productId);

    Task<bool> CreateDiscountAsync(Coupon coupon);

    Task<bool> UpdateDiscountAsync(Coupon coupon);

    Task<bool> DeleteDiscountAsync(string productId);
}