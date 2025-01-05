using Microsoft.AspNetCore.Identity;
using WinBind.Application.Abstractions;
using WinBind.Application.Features.Commands.Requests;
using WinBind.Domain.Entities.Identity;
using WinBind.Domain.Models;
using WinBind.Domain.Models.User;

namespace WinBind.Persistence.Services
{
    public class UserService(UserManager<AppUser> _userManager, RoleManager<AppRole> _roleManager, ITokenService _tokenService) : IUserServive
    {
        public async Task<UserLoginModel> UserLoginAsync(UserLoginCommandRequest userLoginCommand)
        {
            if (userLoginCommand is null)
                throw new NullReferenceException($"{nameof(UserLoginCommandRequest)} is null !");

            AppUser? user = await _userManager.FindByEmailAsync(userLoginCommand.Email);

            if (user is null || user.IsDeleted is true)
                return new()
                {
                    Errors = new string[] { "User Not found" }
                };

            bool result = await _userManager.CheckPasswordAsync(user, userLoginCommand.Password);

            if (!result)
                return new()
                {
                    Errors = new string[] { "Invalid user inputs" }
                };

            IList<string> userRole = await _userManager.GetRolesAsync(user);

            Token? token = _tokenService.GenerateToken(user);

            return new()
            {
                UserModel = new()
                {
                    Id = user.Id,
                    CreatedAtUtc = user.CreatedAtUtc,
                    PhoneNumber = user.PhoneNumber,
                    UserName = user.UserName,
                    Email = user.Email
                },
                Token = token.AccessToken,
                Role = userRole.FirstOrDefault(),
            };
        }

        public async Task<UserRegisterModel> UserRegisterAsync(UserRegisterCommandRequest userRegisterCommand, string? role = "User")
        {
            if (userRegisterCommand is null)
                throw new NullReferenceException($"{nameof(UserRegisterCommandRequest)} is null !");

            AppUser user = new()
            {
                Email = userRegisterCommand.Email,
                PhoneNumber = userRegisterCommand.PhoneNumber,
                UserName = userRegisterCommand.Name,
            };

            IdentityResult result = await _userManager.CreateAsync(user, userRegisterCommand.Password);

            if (result.Succeeded)
            {
                bool userRoleExists = await _roleManager.RoleExistsAsync(role);
                if (!userRoleExists)
                {
                    IdentityResult roleResult = await _roleManager.CreateAsync(new AppRole { Name = role });
                    if (!roleResult.Succeeded)
                    {
                        return new()
                        {
                            IsSuccess = false,
                            Errors = roleResult.Errors.Select(e => e.Description).ToArray()
                        };
                    }
                }

                IdentityResult? addToRoleResult = await _userManager.AddToRoleAsync(user, role);
                return addToRoleResult.Succeeded ?
                    new()
                    {
                        IsSuccess = true,
                        Role = role,
                        UserId = user.Id,
                        Errors = null
                    } :
                    new()
                    {
                        IsSuccess = false,
                        Errors = addToRoleResult.Errors.Select(e => e.Description).ToArray()
                    };
            }
            else
            {
                string[] errorMessages = result.Errors.Select(e => e.Description).ToArray();
                return new()
                {
                    IsSuccess = false,
                    Errors = errorMessages,
                    Role = "User"
                };
            }
        }
    }
}
