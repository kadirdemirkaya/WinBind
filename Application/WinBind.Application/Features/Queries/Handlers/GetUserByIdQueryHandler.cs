using MediatR;
using Microsoft.AspNetCore.Identity;
using WinBind.Application.Features.Queries.Requests;
using WinBind.Domain.Entities.Identity;
using WinBind.Domain.Models.Responses;
using WinBind.Domain.Models.User;

namespace WinBind.Application.Features.Queries.Handlers
{
    public class GetUserByIdQueryHandler(UserManager<AppUser> _userManager) : IRequestHandler<GetUserByIdQueryRequest, ResponseModel<UserModel>>
    {
        public async Task<ResponseModel<UserModel>> Handle(GetUserByIdQueryRequest request, CancellationToken cancellationToken)
        {
            AppUser? appUser = await _userManager.FindByIdAsync(request.Id.ToString());

            if (appUser == null || appUser.IsDeleted == true)
                return new ResponseModel<UserModel>("User not found", 404);

            var userModel = new UserModel
            {
                Id = appUser.Id,
                Email = appUser.Email,
                UserName = appUser.UserName,
                PhoneNumber = appUser.PhoneNumber,
                CreatedAtUtc = appUser.CreatedAtUtc
            };

            return new ResponseModel<UserModel>(userModel);
        }
    }
}
