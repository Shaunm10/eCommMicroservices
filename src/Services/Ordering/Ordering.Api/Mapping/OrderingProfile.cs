using AutoMapper;
using EventBus.Messages.Events;
using Ordering.Application.Features.Orders.Commands.CheckoutOrder;

namespace Ordering.Api.Mapping;

public class OrderingProfile : Profile
{
    public OrderingProfile()
    {
        this.CreateMap<CheckoutOrderCommand, BasketCheckoutEvent>().ReverseMap();
    }
}
