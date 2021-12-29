using AutoMapper;
using Discount.Grpc.Protos;

namespace Discount.Grpc.Mapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        this.CreateMap<Common.Entities.V1.Discount, DiscountModel>()
            .ReverseMap();
    }
}