using Microsoft.AspNetCore.Mvc;
using WinBind.Application.Abstractions;

namespace WinBind.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpPost]
        [Route("user-login")]
        public async Task<IActionResult> UserLogin()
        {
            return Ok();
        }

        [HttpPost]
        [Route("user-register")]
        public async Task<IActionResult> UserRegister()
        {
            return Ok();
        }
    }
}
