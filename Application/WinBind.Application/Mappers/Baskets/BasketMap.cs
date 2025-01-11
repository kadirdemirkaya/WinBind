using AutoMapper;
using WinBind.Domain.Entities;
using WinBind.Domain.Models.Basket;

namespace WinBind.Application.Mappers.Baskets
{
    public class BasketMap : Profile
    {
        public BasketMap()
        {
            CreateMap<BasketItem, BasketItemDto>().ReverseMap();
            CreateMap<Basket, CreateBasketItemDto>().ReverseMap();
            CreateMap<Basket, UserBasketModel>().ReverseMap();
            CreateMap<BasketItem, BasketItemModel>().ReverseMap();
            CreateMap<Basket, UserBasketModel>()
              .ForMember(dest => dest.BasketItemModel, opt => opt.MapFrom(src => src.BasketItems))
              .ReverseMap();
            CreateMap<BasketItemModel, BasketItem>().ReverseMap();
            CreateMap<BasketItem, CreateBasketItemDto>().ReverseMap();
        }
    }
}
