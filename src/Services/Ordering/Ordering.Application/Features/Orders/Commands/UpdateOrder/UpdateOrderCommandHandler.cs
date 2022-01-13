using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Contracts.Persistence;

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
            this._logger.LogError($"Order not found in database with OrderId:{request.Id}");
            //throw new DirectoryNotFoundException();
        }


        return new Unit();
    }
}