using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WinBind.Application.Features.Commands.Requests;
using WinBind.Domain.Models.Order;
using WinBind.Domain.Models.Responses;

namespace WinBind.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController(IMediator _mediator) : ControllerBase
    {
        /// <summary>
        /// userId vermesen de olur (nuulable), token dan ben isteği atan kim biliyorum. BasketItem listesi lazım 
        /// </summary>
        /// <param name="createOrderDto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("create-order")]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDto createOrderDto)
        {
            CreateOrderCommandRequest createOrderCommandRequest = new(createOrderDto);
            ResponseModel<CreateOrderModel> responseModel = await _mediator.Send(createOrderCommandRequest);

            if (responseModel.Success is false)
                return BadRequest(responseModel);

            return Ok(responseModel);
        }
    }
}
