namespace Discount.Grpc.Repositories;

public interface IDiscountRepositoryOLD
{
    Task<Business.Entities.V1.Discount?> GetDiscountAsync(string productId);

    Task<bool> CreateDiscountAsync(Business.Entities.V1.Discount coupon);

    Task<bool> UpdateDiscountAsync(Business.Entities.V1.Discount coupon);

    Task<bool> DeleteDiscountAsync(string productId);
}