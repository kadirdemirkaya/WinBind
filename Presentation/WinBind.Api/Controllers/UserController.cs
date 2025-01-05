using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using WinBind.Application.Features.Commands.Requests;
using WinBind.Application.Features.Queries.Requests;
using WinBind.Domain.Models.Responses;
using WinBind.Domain.Models.User;

namespace WinBind.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IMediator _mediator) : ControllerBase
    {
        [HttpPost]
        [Route("user-login")]
        public async Task<IActionResult> UserLogin([FromBody] UserLoginCommandRequest userLoginCommandRequest)
        {
            ResponseModel<UserLoginModel> responseModel = await _mediator.Send(userLoginCommandRequest);

            if (responseModel.Success is false)
                return BadRequest(responseModel);

            return Ok(responseModel);
        }

        [HttpPost]
        [Route("user-register")]
        public async Task<IActionResult> UserRegister([FromBody] UserRegisterCommandRequest userRegisterCommandRequest)
        {
            ResponseModel<UserRegisterModel> responseModel = await _mediator.Send(userRegisterCommandRequest);

            if (responseModel.Success is false)
                return BadRequest(responseModel);

            return Ok(responseModel);
        }

        [HttpGet]
        [Route("get-user-by-id")]
        public async Task<IActionResult> GetUserById([FromHeader] Guid userId)
        {
            ResponseModel<UserModel> responseModel = await _mediator.Send(new GetUserByIdQueryRequest(userId));

            if (responseModel.Success is false)
                return NotFound(responseModel);

            return Ok(responseModel);
        }

        [HttpDelete]
        [Route("delete-user-by-id")]
        public async Task<IActionResult> DeleteUserById([FromHeader] Guid userId)
        {
            ResponseModel<bool> responseModel = await _mediator.Send(new DeleteUserByIdCommandRequest(userId));

            if (responseModel.Success is true)
                return BadRequest(responseModel);

            return Ok(responseModel);
        }

        [HttpPut]
        [Route("user-update")]
        public async Task<IActionResult> UserUpdate([FromBody] UserUpdateCommandRequest userUpdateCommandRequest)
        {
            ResponseModel<UserModel> responseModel = await _mediator.Send(userUpdateCommandRequest);

            if (responseModel.Success is false)
                return BadRequest(responseModel);

            return Ok(responseModel);

        }
    }
}
