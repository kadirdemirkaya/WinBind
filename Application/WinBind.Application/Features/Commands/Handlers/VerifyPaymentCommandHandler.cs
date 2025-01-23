using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinBind.Application.Abstractions;
using WinBind.Application.Features.Commands.Requests;
using WinBind.Domain.Models.Payment;

namespace WinBind.Application.Features.Commands.Handlers
{
    public class VerifyPaymentCommandHandler : IRequestHandler<VerifyPaymentCommandRequest, VerifyPaymentResponseDto>
    {
        private readonly IPaymentService _paymentService;
        public VerifyPaymentCommandHandler(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        public async Task<VerifyPaymentResponseDto> Handle(VerifyPaymentCommandRequest request, CancellationToken cancellationToken)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var response = await _paymentService.VerifyPaymentAsync(request);
            return response;
        }
    }
}
