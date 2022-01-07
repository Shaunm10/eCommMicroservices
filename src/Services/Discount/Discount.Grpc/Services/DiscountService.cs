using AutoMapper;
using Discount.Business.Repositories;
using Discount.Grpc.Protos;
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

        var discountModel = this._mapper.Map<DiscountModel>(discount);
        return discountModel;
    }

    public override async Task<DiscountList> GetDiscounts(GetDiscountsRequest request, ServerCallContext context)
    {
        var discounts = await this._discountRepository.GetDiscountsAsync(request.ProductIds);

        var discountList = new DiscountList();
        foreach (var discount in discounts) 
        {
            discountList.Discounts.Add(this._mapper.Map<DiscountModel>(discount));
        }

        //var discountModel = this._mapper.Map<DiscountList>(discounts);
        return discountList;
    }

    public async override Task<DiscountModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
    {
        var discount = this._mapper.Map<Business.Entities.V1.Discount>(request.Discount);

        var isSuccess = await this._discountRepository.CreateDiscountAsync(discount);

        if (isSuccess)
        {
            this._logger?.LogInformation($"Discount is successfully created for ProductId: {request.Discount.ProductId}");
            return this._mapper.Map<DiscountModel>(discount);
        }

        var errorMessage = $"Unable to create discount for: {request.Discount?.ProductId}";
        this._logger.LogWarning($"In  {nameof(this.CreateDiscount)} - {errorMessage}");
        throw new RpcException(new Status(StatusCode.Internal, errorMessage));
    }

    override public async Task<DiscountModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
    {
        var discount = this._mapper.Map<Business.Entities.V1.Discount>(request.Discount);

        var isSuccess = await this._discountRepository.UpdateDiscountAsync(discount);

        if (isSuccess)
        {
            this._logger.LogInformation($"Successfully updated discount for productId: {request.Discount.ProductId}");
            return this._mapper.Map<DiscountModel>(discount);
        }

        var errorMessage = $"Unable to update discount for ProductId: {request.Discount?.ProductId}";
        this._logger.LogWarning($"In  {nameof(this.UpdateDiscount)} - {errorMessage}");
        throw new RpcException(new Status(StatusCode.Unknown, errorMessage));
    }

    public async override Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
    {
        var isSuccess = await this._discountRepository.DeleteDiscountAsync(request.ProductId);

        if (isSuccess)
        {
            // pull the newly created Discount and return it.
            var updatedDiscount = await this._discountRepository.GetDiscountAsync(request.ProductId);
            return new DeleteDiscountResponse
            {
                Success = isSuccess
            };
        }

        var errorMessage = $"Unable to delete discount for ProductId: {request.ProductId}";
        this._logger.LogWarning($"In  {nameof(this.DeleteDiscount)} - {errorMessage}");
        throw new RpcException(new Status(StatusCode.Unknown, errorMessage));
    }
}