using AutoMapper;
using WinBind.Application.Features.Commands.Requests;
using WinBind.Domain.Entities.Identity;
using WinBind.Domain.Models.User;

namespace WinBind.Application.Mappers.Users
{
    public class UserMap : Profile
    {
        public UserMap()
        {
            CreateMap<UserUpdateCommandRequest, AppUser>().ReverseMap();
            CreateMap<AppUser, UserModel>().ReverseMap();
        }
    }
}
