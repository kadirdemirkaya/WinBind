using MediatR;
using WinBind.Application.Abstractions;
using WinBind.Application.Features.Commands.Requests;
using WinBind.Domain.Models.Responses;
using WinBind.Domain.Models.User;

namespace WinBind.Application.Features.Commands.Handlers
{
    public class UserRegisterCommandHandler(IUserServive _userServive) : IRequestHandler<UserRegisterCommandRequest, ResponseModel<UserRegisterModel>>
    {
        public async Task<ResponseModel<UserRegisterModel>> Handle(UserRegisterCommandRequest request, CancellationToken cancellationToken)
        {
            UserRegisterModel result = await _userServive.UserRegisterAsync(request);

            if(result.Errors.Any())
                return new ResponseModel<UserRegisterModel>(result.Errors, 400);

            return new ResponseModel<UserRegisterModel>(result);
        }
    }
}
