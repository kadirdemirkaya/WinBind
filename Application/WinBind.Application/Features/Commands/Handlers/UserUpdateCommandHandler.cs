using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using WinBind.Application.Features.Commands.Requests;
using WinBind.Domain.Entities.Identity;
using WinBind.Domain.Models.Responses;
using WinBind.Domain.Models.User;

namespace WinBind.Application.Features.Commands.Handlers
{
    public class UserUpdateCommandHandler(UserManager<AppUser> _userManager, IMapper _mapper) : IRequestHandler<UserUpdateCommandRequest, ResponseModel<UserModel>>
    {
        public async Task<ResponseModel<UserModel>> Handle(UserUpdateCommandRequest request, CancellationToken cancellationToken)
        {
            var existingUser = await _userManager.FindByIdAsync(request.Id.ToString());

            if (existingUser == null)
            {
                return new ResponseModel<UserModel>("User not found", 404);
            }

            _mapper.Map(request, existingUser);

            if (string.IsNullOrEmpty(existingUser.SecurityStamp))
                existingUser.SecurityStamp = Guid.NewGuid().ToString();

            IdentityResult result = await _userManager.UpdateAsync(existingUser);

            if (!result.Succeeded)
                return new ResponseModel<UserModel>(string.Join(", ", result.Errors.Select(e => e.Description)), 400);

            return new ResponseModel<UserModel>(_mapper.Map<AppUser, UserModel>(existingUser));
        }
    }
}
