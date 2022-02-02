using AutoMapper;
using Basket.Api.Entities.V1;
using EventBus.Messages.Events;

namespace Basket.Api.Mapper;

public class BasketProfile : Profile
{
    public BasketProfile()
    {
        this.CreateMap<BasketCheckout, BasketCheckoutEvent>().ReverseMap();
    }
}