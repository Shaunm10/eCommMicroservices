using Discount.Api.Entities.V1;

namespace Discount.Api.Repositories;

public interface IDiscountRepository
{
    Task<Coupon> GetDiscount(int productId);

    Task<bool> CreateDiscount(Coupon coupon);

    Task<bool> UpdateDiscount(Coupon coupon);

    Task<bool> DeleteDiscount(int productId);


}

