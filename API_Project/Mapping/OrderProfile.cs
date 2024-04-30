using AutoMapper;
using Core.DataTransferObjects.Order;
using Core.Models.Order;
using Core.Models.User;

namespace API_Project.Mapping
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Address , AddressDto>().ReverseMap();

            CreateMap<OrderItem , OrderItemDto>().ReverseMap();

            CreateMap<Order, OrderResultDto>()
                .ForMember(dest => dest.DeliveryMethod, opt => opt.MapFrom(src => src.DeliveryMethod.ShortName))
                .ForMember(dest => dest.DeliveryPrice, opt => opt.MapFrom(src => src.DeliveryMethod.Price));

        }
    }
}
