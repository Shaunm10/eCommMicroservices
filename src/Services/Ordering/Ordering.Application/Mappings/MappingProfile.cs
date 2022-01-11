using AutoMapper;
using Ordering.Application.Features.Orders.Queries.GetOrdersList;

namespace Ordering.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        this.CreateMap<Domain.Entities.Order, OrderVM>().ReverseMap();
    }
}
