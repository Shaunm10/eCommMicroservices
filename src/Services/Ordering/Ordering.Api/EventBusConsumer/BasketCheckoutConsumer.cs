using AutoMapper;
using EventBus.Messages.Events;
using MassTransit;
using MediatR;
using Ordering.Application.Features.Orders.Commands.CheckoutOrder;

namespace Ordering.Api.EventBusConsumer;

public class BasketCheckoutConsumer : IConsumer<BasketCheckoutEvent>
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly ILogger<BasketCheckoutConsumer> _logger;

    public BasketCheckoutConsumer(IMapper mapper, IMediator mediator, ILogger<BasketCheckoutConsumer> logger)
    {
        this._mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        this._mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task Consume(ConsumeContext<BasketCheckoutEvent> context)
    {
        try
        {
            var command = this._mapper.Map<CheckoutOrderCommand>(context.Message);
            var newOrderId = await this._mediator.Send(command);

            // if successful, log out the OrderId
            this._logger.LogInformation($"{nameof(BasketCheckoutConsumer)} consumed successfully. Created Order Id: {newOrderId}");
        }
        catch (Exception ex)
        {
            // if not log out the exception that occurred.
            this._logger.LogError(ex, "Unable to finish Order execution from MessageEvent.");
        }
    }
}