using Discount.Grpc.Protos;
using Discount.Grpc.Repositories;
using Grpc.Core;
using static Discount.Grpc.Protos.DiscountProtoService;

namespace Discount.Grpc.Services;

public class DiscountService : DiscountProtoServiceBase
{
    private readonly IDiscountRepository _discountRepository;
    private readonly ILogger<DiscountService> _logger;

    public DiscountService(IDiscountRepository discountRepository, ILogger<DiscountService> logger)
    {
        this._discountRepository = discountRepository ?? throw new ArgumentException(nameof(discountRepository));
        this._logger = logger ?? throw new ArgumentException(nameof(logger));
    }

    public async override Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
    {
        var discount = await this._discountRepository.GetDiscountAsync(request.ProductId);
        if (discount != null)
        {
            return new CouponModel
            {
                Amount = (float)discount.Amount,
                Description = discount.Description,
                Id = discount.Id,
                ProductId = discount.ProductId
            };
        }

        return null;
    }
}