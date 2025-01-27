using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WinBind.Application.Features.Commands.Requests;

namespace WinBind.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class EmailController : ControllerBase
    {
        private readonly IMediator _mediator;
        public EmailController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost("SendEmail")]
        public async Task<IActionResult> SendEmail(SendEmailCommandRequest command)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _mediator.Send(command);
            if(result.Success is false)
                return BadRequest(ModelState);

            return Ok("Email başarıyla gönderildi.");   
        }
    }
}
