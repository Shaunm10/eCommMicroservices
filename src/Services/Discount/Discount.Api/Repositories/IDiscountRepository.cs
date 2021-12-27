﻿using Discount.Api.Entities.V1;

namespace Discount.Api.Repositories;

public interface IDiscountRepository
{
    Task<Entities.V1.Discount?> GetDiscountAsync(string productId);

    Task<bool> CreateDiscountAsync(Entities.V1.Discount coupon);

    Task<bool> UpdateDiscountAsync(Entities.V1.Discount coupon);

    Task<bool> DeleteDiscountAsync(string productId);
}