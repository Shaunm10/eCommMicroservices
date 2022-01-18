using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Contracts.Persistence;
using Ordering.Application.Exceptions;
using Ordering.Domain.Entities;

namespace Ordering.Application.Features.Orders.Commands.DeleteOrder;

public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<DeleteOrderCommandHandler> _logger;

    public DeleteOrderCommandHandler(IOrderRepository orderRepository, IMapper mapper, ILogger<DeleteOrderCommandHandler> logger)
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
            throw new NotFoundException(nameof(Order), request.Id);
        }
        else
        {
            await this._orderRepository.DeleteAsync(existingOrder);
            this._logger.LogInformation("Order {Id} is successfully deleted", request.Id);
        }

        return Unit.Value;
    }
}