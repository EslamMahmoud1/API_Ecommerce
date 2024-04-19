using API_Project.Helper;
using AutoMapper;
using Core.DataTransferObjects;
using Core.Models;
using Core.Models.Basket;

namespace Services.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductToReturnDto>()
                .ForMember(d => d.BrandName, o => o.MapFrom(s => s.productBrand.Name))
                .ForMember(d => d.TypeName, o => o.MapFrom(s => s.productType.Name))
                .ForMember(d => d.PictureUrl, o => o.MapFrom<PictureUrlResolver>());

            CreateMap<ProductBrand, BrandTypeDto>();
            CreateMap<ProductType, BrandTypeDto>();


            CreateMap<Basket,BasketDto>().ReverseMap();
            CreateMap<BasketItem, BasketItemDto>().ReverseMap();
        }
    }
}
