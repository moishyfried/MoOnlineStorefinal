using AutoMapper;
using MoOnlineStore.Api.Helpers;
using MoOnlineStore.Core.DTO;
using MoOnlineStore.Core.DTO.identity;
using MoOnlineStore.Core.EntityClasses;
using MoOnlineStore.Core.EntityClasses.OrderAggregate;
using MoOnlineStore.Core.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoOnlineStore.Core.Helpers
{
  public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Products, DTOProducts>()
             .ForMember(d => d.ProductBrand, o => o.MapFrom(s => s.ProductBrand.Name))
            .ForMember(d => d.ProductType, o => o.MapFrom(s => s.ProductType.Name))
            .ForMember(d => d.PictureUrl, o => o.MapFrom<ProductUrlResolver>());
             CreateMap<ProductBrand, DTOProductBrand>();
              CreateMap<ProductType, DTOProductType>();
              CreateMap<AppUser, UserDto>(); 
               CreateMap<Identity.Address, AddressDto>().ReverseMap();
            CreateMap<EntityClasses.OrderAggregate.Address, AddressDto>().ReverseMap();
            CreateMap<BasketDto, CustomerBasketEntity>();
             CreateMap<BasketItemDto, BasketItemsEntity>();
            CreateMap<Order, OrderToReturnDto>().
                ForMember(s => s.DeliveryMethod, o => o.MapFrom(s => s.DeliveryMethod.ShortName)).
                ForMember(s => s.ShippingPrice, o => o.MapFrom(s => s.DeliveryMethod.Price));
            CreateMap<OrderItem, OrderItemDto>().
                ForMember(s => s.ProductId, o => o.MapFrom(s => s.ItemOrdered.ProductItemId)).
                ForMember(s => s.ProductName, o => o.MapFrom(s => s.ItemOrdered.ProductName)).
                ForMember(s => s.PictureUrl, o => o.MapFrom(s => s.ItemOrdered.PictureUrl)).
                ForMember(s => s.PictureUrl, o => o.MapFrom<OrderItemUrlResolver>());
        }
    }
}
