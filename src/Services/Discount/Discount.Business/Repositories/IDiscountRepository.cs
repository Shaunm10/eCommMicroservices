namespace Discount.Business.Repositories;

public interface IDiscountRepository
{
    Task<Entities.V1.Discount?> GetDiscountAsync(string productId);

    Task<IEnumerable<Entities.V1.Discount>> GetDiscountsAsync(IEnumerable<string> productIds);

    Task<bool> CreateDiscountAsync(Entities.V1.Discount coupon);

    Task<bool> UpdateDiscountAsync(Entities.V1.Discount coupon);

    Task<bool> DeleteDiscountAsync(string productId);
}