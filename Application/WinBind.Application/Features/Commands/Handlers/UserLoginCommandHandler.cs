using MediatR;
using WinBind.Application.Abstractions;
using WinBind.Application.Features.Commands.Requests;
using WinBind.Domain.Models.Responses;
using WinBind.Domain.Models.User;

namespace WinBind.Application.Features.Commands.Handlers
{
    public class UserLoginCommandHandler(IUserServive _userServive) : IRequestHandler<UserLoginCommandRequest, ResponseModel<UserLoginModel>>
    {
        public async Task<ResponseModel<UserLoginModel>> Handle(UserLoginCommandRequest request, CancellationToken cancellationToken)
        {
            UserLoginModel result = await _userServive.UserLoginAsync(request);

            return new ResponseModel<UserLoginModel>(result);
        }
    }
}
