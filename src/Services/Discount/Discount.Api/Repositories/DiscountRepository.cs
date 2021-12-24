using Discount.Api.Entities.V1;

namespace Discount.Api.Repositories
{
    public class DiscountRepository : IDiscountRepository
    {
        public Task<bool> CreateDiscount(Coupon coupon)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteDiscount(int productId)
        {
            throw new NotImplementedException();
        }

        public Task<Coupon> GetDiscount(int productId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateDiscount(Coupon coupon)
        {
            throw new NotImplementedException();
        }
    }
}
