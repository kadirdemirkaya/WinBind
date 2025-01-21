using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinBind.Application.Features.Commands.Requests;
using WinBind.Domain.Entities;
using WinBind.Domain.Entities.Identity;
using WinBind.Domain.Models.Product;

namespace WinBind.Application.Mappers.Products
{
    public class ProductMap : Profile
    {
        public ProductMap()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(p => p.ProductImages, opt => opt.MapFrom(src => src.ProductImages))
                .ReverseMap();

            CreateMap<Product, UpdateProductCommandRequest>().ReverseMap();

        }
    }
}
