using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WinBind.Application.Abstractions;
using WinBind.Application.Features.Commands.Requests;
using WinBind.Application.Features.Queries.Handlers;
using WinBind.Application.Features.Queries.Requests;
using WinBind.Domain.Models.Basket;
using WinBind.Domain.Models.Responses;
using WinBind.Domain.Models.User;

namespace WinBind.Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMediator _mediator;
        private readonly ITokenService _tokenService;

        public ProductController(IHttpContextAccessor httpContextAccessor, IMediator mediator, ITokenService tokenService)
        {
            _httpContextAccessor = httpContextAccessor;
            _mediator = mediator;
            _tokenService = tokenService;
        }

        [HttpGet("get-product-by-id")]
        public async Task<IActionResult> GetProductById(Guid? productId)
        {
            if (productId is null)
                productId = Guid.Parse(_tokenService.GetClaimFromRequest(_httpContextAccessor.HttpContext, "Id"));

            var response = await _mediator.Send(new GetProductByIdQueryRequest((Guid)productId));

            if (response.Success is false)
                return NotFound(response);

            return Ok(response);
        }

        [HttpGet("get-product-list")]
        public async Task<IActionResult> GetProductList([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var response = await _mediator.Send(new GetAllProductsQueryRequest(page,pageSize));

            if (response.Success is false)
                return NotFound(response);

            return Ok(response);
        }


        [Authorize]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("create-product")]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductCommandRequest request)
        {
            var response = await _mediator.Send(request);

            if(response.Success is false)
                return BadRequest(response);

            return Ok(response);
        }

        [Authorize]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut("update-product")]
        public async Task<IActionResult> UpdateProduct([FromBody] UpdateProductCommandRequest request)
        {
            var response = await _mediator.Send(request);

            if (response.Success is false)
                return BadRequest(response);

            return Ok(response);
        }

        [Authorize]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpDelete("delete-product-by-id")]
        public async Task<IActionResult> DeleteProductById([FromHeader] Guid productId)
        {
            ResponseModel<bool> responseModel = await _mediator.Send(new DeleteProductByIdCommandRequest(productId));

            if (responseModel.Success is false)
                return BadRequest(responseModel);

            return Ok(responseModel);
        }

        /// <summary>
        /// sortOrder kullanılacak ise "asc" veya "desc" yazılması gerek yani descending veya ascending
        /// </summary>        
        [HttpPost("get-filtered-and-sorted-products")]
        public async Task<IActionResult> GetFilteredAndSortedProducts([FromBody] GetFilteredAndSortedProductsQueryRequest request)
        {
            var response = await _mediator.Send(new GetFilteredAndSortedProductsQueryRequest(request.Page, request.PageSize, request.Brand, request.BandColor, request.CaseColor, request.SortOrder));

            if (response.Success is false)
                return BadRequest(response);

            return Ok(response);
        }
    }
}
