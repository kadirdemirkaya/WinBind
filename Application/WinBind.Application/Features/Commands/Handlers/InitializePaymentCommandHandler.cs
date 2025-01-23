using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinBind.Application.Abstractions;
using WinBind.Application.Features.Commands.Requests;
using WinBind.Domain.Models.Payment;
using WinBind.Domain.Models.Responses;

namespace WinBind.Application.Features.Commands.Handlers
{
    public class InitializePaymentCommandHandler : IRequestHandler<InitializePaymentCommandRequest, InitializePaymentResponseDto>
    {
        private readonly IPaymentService _paymentService;
        public InitializePaymentCommandHandler(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        public async Task<InitializePaymentResponseDto> Handle(InitializePaymentCommandRequest request, CancellationToken cancellationToken)
        {
            if(request == null)
                throw new ArgumentNullException(nameof(request));

            var response = await _paymentService.ProcessPaymentAsync(request);
            return response;
        }
    }
}
