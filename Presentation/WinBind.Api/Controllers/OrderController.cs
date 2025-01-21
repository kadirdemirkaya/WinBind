using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WinBind.Application.Abstractions;
using WinBind.Application.Features.Commands.Requests;
using WinBind.Application.Features.Queries.Requests;
using WinBind.Domain.Entities;
using WinBind.Domain.Models.Order;
using WinBind.Domain.Models.Responses;

namespace WinBind.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class OrderController(IMediator _mediator, ITokenService _tokenService, IHttpContextAccessor _httpContextAccessor) : ControllerBase
    {
        /// <summary>
        /// userId vermesen de olur (nuulable), token dan ben isteği atan kim biliyorum. BasketItem listesi lazım (token'a gerek var)
        /// </summary>
        /// <param name="createOrderDto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("create-order")]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDto createOrderDto)
        {
            if (createOrderDto.UserId is null)
                createOrderDto.UserId = Guid.Parse(_tokenService.GetClaimFromRequest(_httpContextAccessor.HttpContext, "Id"));

            CreateOrderCommandRequest createOrderCommandRequest = new(createOrderDto);
            ResponseModel<CreateOrderModel> responseModel = await _mediator.Send(createOrderCommandRequest);

            if (responseModel.Success is false)
                return BadRequest(responseModel);

            return Ok(responseModel);
        }

        /// <summary>
        /// orderId ye göre order silme (token'a gere yok)
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        [HttpDelete]
        [AllowAnonymous]
        [Route("delete-order")]
        public async Task<IActionResult> DeleteOrder([FromHeader] Guid? orderId)
        {
            DeleteOrderCommandRequest deleteOrderCommandRequest = new(orderId);
            ResponseModel<bool> responseModel = await _mediator.Send(deleteOrderCommandRequest);

            if (responseModel.Success is false)
                return BadRequest(responseModel);

            return Ok(responseModel);
        }

        /// <summary>
        /// userID ye ait tüm order'ları getirir. Pagination var (token lazım)
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("get-all-orders")]
        public async Task<IActionResult> GetAllOrders([FromHeader] Guid? userId, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            if (userId == null)
                userId = Guid.Parse(_tokenService.GetClaimFromRequest(_httpContextAccessor.HttpContext, "Id"));

            GetAllOrdersQueryRequest getAllOrdersQueryRequest = new(userId, page, pageSize);
            ResponseModel<List<GetAllOrderModel>> responseModel = await _mediator.Send(getAllOrdersQueryRequest);

            if (responseModel.Success is false)
                return BadRequest(responseModel);

            return Ok(responseModel);
        }

        /// <summary>
        /// userId'ye ait aktif veya aktif olmayan order'ları getirir. Pagination var (token lazım)
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="isDeleted"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("get-all-order-by-active")]
        public async Task<IActionResult> GetAllOrderByActive([FromHeader] Guid? userId, [FromQuery] bool isDeleted = true, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            if (userId == null)
                userId = Guid.Parse(_tokenService.GetClaimFromRequest(_httpContextAccessor.HttpContext, "Id"));

            GetAllOrderByActiveQueryRequest getAllOrderByActiveQueryRequest = new(userId, page, pageSize, isDeleted);
            ResponseModel<List<GetAllOrderModel>> responseModel = await _mediator.Send(getAllOrderByActiveQueryRequest);

            if (responseModel.Data.Count() == 0)
                return NotFound(responseModel);

            return Ok(responseModel);
        }
    }
}
