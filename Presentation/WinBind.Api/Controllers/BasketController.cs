﻿using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WinBind.Application.Abstractions;
using WinBind.Application.Features.Commands.Requests;
using WinBind.Application.Features.Queries.Requests;
using WinBind.Domain.Models.Basket;
using WinBind.Domain.Models.Responses;

namespace WinBind.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class BasketController(IMediator _mediator, ITokenService _tokenService, IHttpContextAccessor _httpContextAccessor) : ControllerBase
    {
        [HttpPost]
        [Route("create-basket")]
        public async Task<IActionResult> CreateBasket([FromBody] CreateBasketDto createBasketDto)
        {
            if (createBasketDto.UserId is null)
                createBasketDto.UserId = Guid.Parse(_tokenService.GetClaimFromRequest(_httpContextAccessor.HttpContext, "Id"));

            CreateBasketCommandRequest createBasketCommandRequest = new(createBasketDto);
            ResponseModel<bool> response = await _mediator.Send(createBasketCommandRequest);

            if (response.Success is false)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPost]
        [Route("add-to-basket")]
        public async Task<IActionResult> AddToBasket([FromHeader] Guid? userId, [FromBody] BasketItemDto basketItemDto)
        {
            if (userId is null)
                userId = Guid.Parse(_tokenService.GetClaimFromRequest(_httpContextAccessor.HttpContext, "Id"));

            AddToBasketCommandRequest addToBasketCommandRequest = new(userId, basketItemDto);
            ResponseModel<bool> response = await _mediator.Send(addToBasketCommandRequest);

            if (response.Success is false)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpGet]
        [Route("get-user-basket")]
        public async Task<IActionResult> GetUserBasket([FromHeader] Guid? userId)
        {
            if (userId is null)
                userId = Guid.Parse(_tokenService.GetClaimFromRequest(_httpContextAccessor.HttpContext, "Id"));

            GetUserBasketQueryRequest getUserBasketQueryRequest = new(userId);
            ResponseModel<UserBasketModel> response = await _mediator.Send(getUserBasketQueryRequest);

            if (response.Success is false)
                return NotFound(response);

            return Ok(response);
        }

        [HttpDelete]
        [Route("delete-basket-by-id")]
        public async Task<IActionResult> DeleteBasketById([FromHeader] Guid? userId)
        {
            if (userId is null)
                userId = Guid.Parse(_tokenService.GetClaimFromRequest(_httpContextAccessor.HttpContext, "Id"));

            DeleteBasketByIdCommandRequest deleteBasketByIdCommandRequest = new(userId);
            ResponseModel<bool> response = await _mediator.Send(deleteBasketByIdCommandRequest);

            if (response.Success is false)
                return NotFound(response);

            return Ok(response);
        }

        [HttpDelete]
        [Route("delete-basket-item-by-basketid")]
        public async Task<IActionResult> DeleteBasketItemByBasketId([FromHeader] Guid basketItemId, [FromQuery] int deleteCount = 1)
        {
            DeleteBasketItemByBasketIdCommandRequest deleteBasketItemByBasketIdCommandRequest = new(basketItemId, deleteCount);
            ResponseModel<bool> response = await _mediator.Send(deleteBasketItemByBasketIdCommandRequest);

            if (response.Success is false)
                return NotFound(response);

            return Ok(response);
        }
    }
}
