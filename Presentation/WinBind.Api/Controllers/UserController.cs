using Microsoft.AspNetCore.Mvc;
using WinBind.Application.Abstractions;

namespace WinBind.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IPaginationService paginationService) : ControllerBase
    {
        [HttpGet]
        [Route("pagination-request")]
        public async Task<IActionResult> PaginationReq()
        {
            return Ok(paginationService.GetPaginationOptions());
        }
    }
}
