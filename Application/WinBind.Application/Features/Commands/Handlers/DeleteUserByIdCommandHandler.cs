using MediatR;
using Microsoft.AspNetCore.Identity;
using WinBind.Application.Features.Commands.Requests;
using WinBind.Domain.Entities.Identity;
using WinBind.Domain.Models.Responses;

namespace WinBind.Application.Features.Commands.Handlers
{
    public class DeleteUserByIdCommandHandler(UserManager<AppUser> _userManager) : IRequestHandler<DeleteUserByIdCommandRequest, ResponseModel<bool>>
    {
        public async Task<ResponseModel<bool>> Handle(DeleteUserByIdCommandRequest request, CancellationToken cancellationToken)
        {
            AppUser? appUser = await _userManager.FindByIdAsync(request.Id.ToString());

            if (appUser is not null)
            {
                appUser.IsDeleted = true;

                IdentityResult result = await _userManager.UpdateAsync(appUser);

                if (result.Succeeded)
                    return new ResponseModel<bool>(true);

                return new ResponseModel<bool>(string.Join(", ", result.Errors.Select(e => e.Description)), 400);
            }
            return new ResponseModel<bool>("User not found", 404);
        }
    }
}
