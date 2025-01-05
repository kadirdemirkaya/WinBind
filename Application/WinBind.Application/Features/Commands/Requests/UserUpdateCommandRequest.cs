using MediatR;
using WinBind.Domain.Models.Responses;
using WinBind.Domain.Models.User;

namespace WinBind.Application.Features.Commands.Requests
{
    public class UserUpdateCommandRequest : IRequest<ResponseModel<UserModel>>
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
    }
}
