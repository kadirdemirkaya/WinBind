using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WinBind.Application.Abstractions;
using WinBind.Application.Features.Commands.Requests;
using WinBind.Application.Features.Queries.Requests;
using WinBind.Domain.Models.Responses;

namespace WinBind.Api.Controllers
{
    [Authorize]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMediator _mediator;
        private readonly ITokenService _tokenService;
        public CategoryController(IHttpContextAccessor httpContextAccessor, IMediator mediator, ITokenService tokenService)
        {
            _httpContextAccessor = httpContextAccessor;
            _mediator = mediator;
            _tokenService = tokenService;
        }

        [AllowAnonymous]
        [HttpGet("get-category-by-id")]
        public async Task<IActionResult> GetCategoryById(Guid? categoryId)
        {
            if (categoryId is null)
                categoryId = Guid.Parse(_tokenService.GetClaimFromRequest(_httpContextAccessor.HttpContext, "Id"));

            var response = await _mediator.Send(new GetCategoryByIdQueryRequest((Guid)categoryId));

            if (response.Success is false)
                return BadRequest(response);

            return Ok(response);
        }

       [HttpPost("create-category")]
       public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryCommandRequest request)
       {
           var response = await _mediator.Send(request);

           if (response.Success is false)
               return BadRequest(response);

           return Ok(response);
       }

        [HttpPut("update-category")]
        public async Task<IActionResult> UpdateCategory([FromBody] UpdateCategoryCommandRequest request)
        {
            var response = await _mediator.Send(request);

            if (response.Success is false)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpDelete("delete-category-by-id")]
        public async Task<IActionResult> DeleteCategoryById([FromHeader] Guid categoryId)
        {
            ResponseModel<bool> responseModel = await _mediator.Send(new DeleteCategoryByIdCommandRequest(categoryId));

            if (responseModel.Success is false)
                return BadRequest(responseModel);

            return Ok(responseModel);
        }

        [AllowAnonymous]
        [HttpGet("get-category-list")]
        public async Task<IActionResult> GetCategoryList()
        {
            GetAllCategoriesQueryRequest request = new();
            var response = await _mediator.Send(request);

            if (response.Success is false)
                return BadRequest(response);

            return Ok(response);
        }
    }
}
