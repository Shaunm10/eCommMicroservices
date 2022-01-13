using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Contracts.Persistence;

namespace Ordering.Application.Features.Orders.Commands.DeleteOrder;

public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<DeleteOrderCommandHandler> _logger;

    public DeleteOrderCommandHandler(IOrderRepository orderRepository, IMapper mapper, ILogger<DeleteOrderCommandHandler> logger )
    {
        this._orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
        this._mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<Unit> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
    {
        var existingOrder = await this._orderRepository.GetByIdAsync(request.Id);
        if (existingOrder is null)
        {
            this._logger.LogWarning("Unable to delete order with orderId {Id} because it's not found.", request.Id);
        }
        else 
        {
            await this._orderRepository.DeleteAsync(existingOrder);
        }

        return Unit.Value;
    }
}