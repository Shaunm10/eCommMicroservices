using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Contracts.Infrastructure;
using Ordering.Application.Contracts.Persistence;
using Ordering.Application.Models;
using Ordering.Domain.Entities;

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

    public async Task<int> Handle(CheckoutOrderCommand request, CancellationToken cancellationToken)
    {
        var order = this.mapper.Map<Order>(request);
        var newOrder = await this.orderRepository.AddAsync(order);

        await this.SendEmail(newOrder);

        return newOrder.Id;
    }

    private async Task SendEmail(Order newOrder)
    {
        var email = new Email
        {
            To = "me@example.com",
            Body = $"Order Id {newOrder.Id} was created.",
            Subject = $"Order Id: {newOrder.Id} was created for {newOrder.UserName}"
        };

        try
        {
            await this.mailService.SendEmailAsync(email);
        }
        catch (Exception ex)
        {
            this.logger.LogError(ex, $"OrderId: {newOrder.Id} failed to send email with email service.");
        }
    }
}
