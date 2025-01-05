using WinBind.Application.Features.Commands.Requests;
using WinBind.Domain.Models.User;

namespace WinBind.Application.Abstractions
{
    public interface IUserServive
    {
        Task<UserRegisterModel> UserRegisterAsync(UserRegisterCommandRequest userRegisterCommand, string? role = "User");

        Task<UserLoginModel> UserLoginAsync(UserLoginCommandRequest userLoginCommand);
    }
}
