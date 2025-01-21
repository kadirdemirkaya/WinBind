using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinBind.Application.Features.Commands.Requests;
using WinBind.Domain.Entities;
using WinBind.Domain.Models.Category;

namespace WinBind.Application.Mappers.Categories
{
    public class CategoryMap : Profile
    {
        public CategoryMap()
        {
            CreateMap<Category, CategoryDto>().ReverseMap();

            CreateMap<UpdateCategoryCommandRequest, Category>().ReverseMap();
        }
    }
}
