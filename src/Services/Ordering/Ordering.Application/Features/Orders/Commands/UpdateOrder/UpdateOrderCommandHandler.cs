using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Contracts.Persistence;
using Ordering.Application.Exceptions;
using Ordering.Domain.Entities;

namespace Ordering.Application.Features.Orders.Commands.UpdateOrder;

public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<UpdateOrderCommandHandler> _logger;

    public UpdateOrderCommandHandler(IOrderRepository orderRepository, IMapper mapper, ILogger<UpdateOrderCommandHandler> logger)
    {
        this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
        this._mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        this._orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
    }

    public async Task<Unit> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        var existingOrder = await this._orderRepository.GetByIdAsync(request.Id);

        if (existingOrder == null)
        {
            throw new NotFoundException(nameof(Order), request.Id);
        }

        // this is acting like a JavaScript spread operation
        this._mapper.Map(request, existingOrder, typeof(UpdateOrderCommand), typeof(Order));

        await this._orderRepository.UpdateAsync(existingOrder);

        return Unit.Value;
    }
}