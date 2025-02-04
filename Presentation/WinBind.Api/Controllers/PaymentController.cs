﻿using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WinBind.Application.Features.Commands.Requests;
using WinBind.Domain.Models.Responses;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace WinBind.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IMediator _mediator;
        public PaymentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// istek atarken Buyer bilgisi - OrderId - BasketId - Price lazım. Response olarak IYZICO token ve hazır Iyzico form html'i return edilir.
        /// </summary> 
        [HttpPost("Initialize-Payment")]
        public async Task<IActionResult> InitializePayment([FromBody] InitializePaymentCommandRequest command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (command == null)
                return NotFound();

            var result = await _mediator.Send(command);

            if (result.IsSuccess)
            {
                var createPaymentCommand2 = new CreatePaymentCommandRequest
                {
                    PaymentId = Guid.NewGuid(),
                    OrderId = Guid.Parse(command.OrderId),
                    Amount = command.Price,
                    PaymentDate = DateTime.UtcNow,
                    PaymentMethod = "Credit Card",
                };

                var createPaymentResult2 = await _mediator.Send(createPaymentCommand2);

                return Ok(result);
            }
            return BadRequest();
        }



        /// <summary>
        /// istek atarken iyzico token lazım. Response da ise tüm payment bilgileri döner. Ve payment create edilir.
        /// </summary> 
        [HttpPost("Verify-Payment")]
        public async Task<IActionResult> PaymentCallback([FromForm] VerifyPaymentCommandRequest command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _mediator.Send(command);

            if (result.PaymentStatus == "SUCCESS")
            {
                var createPaymentCommand = new CreatePaymentCommandRequest
                {
                    PaymentId = result.PaymentId,
                    OrderId = result.OrderId,
                    Amount = decimal.Parse(result.Amount),
                    PaymentDate = result.PaymentDate,
                    PaymentMethod = result.PaymentMethod
                };

                var createPaymentResult = await _mediator.Send(createPaymentCommand);

                return Ok(new
                {
                    Payment = result,
                    CreatePayment = createPaymentResult
                });
            }

            var createPaymentCommand2 = new CreatePaymentCommandRequest
            {
                PaymentId = result.PaymentId,
                OrderId = result.OrderId,
                Amount = decimal.Parse(result.Amount),
                PaymentDate = result.PaymentDate,
                PaymentMethod = result.PaymentMethod
            };

            var createPaymentResult2 = await _mediator.Send(createPaymentCommand2);
            return Ok(new
            {
                Payment = result,
                CreatePayment = createPaymentResult2
            });
        }

        [HttpPost("Create-Payment")]
        public async Task<IActionResult> CreatePayment([FromBody] CreatePaymentCommandRequest command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _mediator.Send(command);

            if (result.Success)
                return Ok(result);

            else
                return BadRequest();
        }
    }
}
