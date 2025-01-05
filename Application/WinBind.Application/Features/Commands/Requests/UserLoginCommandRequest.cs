using MediatR;
using System.ComponentModel.DataAnnotations;
using WinBind.Domain.Models.Responses;
using WinBind.Domain.Models.User;

namespace WinBind.Application.Features.Commands.Requests
{
    public class UserLoginCommandRequest : IRequest<ResponseModel<UserLoginModel>>
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }
}
