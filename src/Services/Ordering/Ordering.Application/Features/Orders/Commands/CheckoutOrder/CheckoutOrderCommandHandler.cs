using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Contracts.Infrastructure;
using Ordering.Application.Contracts.Persistence;

namespace Ordering.Application.Features.Orders.Commands.CheckoutOrder;

public class CheckoutOrderCommandHandler : IRequestHandler<CheckoutOrderCommand, int>
{
    private readonly IOrderRepository orderRepository;
    private readonly IMapper mapper;
    private readonly IEmailService mailService;
    private readonly ILogger<CheckoutOrderCommandHandler> logger;

    public CheckoutOrderCommandHandler(IOrderRepository orderRepository, IMapper mapper, IEmailService mailService, ILogger<CheckoutOrderCommandHandler> logger)
    {
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        this.orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
        this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        this.mailService = mailService ?? throw new ArgumentNullException(nameof(mailService));
    }
    public Task<int> Handle(CheckoutOrderCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
