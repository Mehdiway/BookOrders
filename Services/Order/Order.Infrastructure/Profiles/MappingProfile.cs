using AutoMapper;
using Order.Domain.Entities;
using Shared.DTO;

namespace Order.Infrastructure.Profiles;
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Domain.Entities.Order, OrderDto>().ReverseMap();
        CreateMap<OrderItem, OrderItemDto>().ReverseMap();

        CreateMap<Domain.Entities.Order, Domain.Entities.Order>();
        CreateMap<OrderItem, OrderItem>();
    }
}
