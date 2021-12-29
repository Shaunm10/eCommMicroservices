using AutoMapper;
using Discount.Common.Repositories;
using Discount.Grpc.Protos;
using Discount.Grpc.Repositories;
using Grpc.Core;
using static Discount.Grpc.Protos.DiscountProtoService;

namespace Discount.Grpc.Services;

public class DiscountService : DiscountProtoServiceBase
{
    private readonly IDiscountRepository _discountRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<DiscountService> _logger;

    public DiscountService(IDiscountRepository discountRepository, ILogger<DiscountService> logger, IMapper mapper)
    {
        this._discountRepository = discountRepository ?? throw new ArgumentNullException(nameof(discountRepository));
        this._mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public override async Task<DiscountModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
    {
        var discount = await this._discountRepository.GetDiscountAsync(request.ProductId);

        if (discount == null)
        {
            var errorMessage = $"Discount with productId: {request.ProductId} is not found";
            this._logger.LogWarning($"In  {nameof(this.GetDiscount)} - {errorMessage}");
            throw new RpcException(new Status(StatusCode.NotFound, errorMessage));
        }

        return new DiscountModel();
       // var discountModel = _mapper.Map<CouponModel>(discount);
        //return discountModel;
    }
}