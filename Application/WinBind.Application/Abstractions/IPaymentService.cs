using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinBind.Application.Features.Commands.Requests;
using WinBind.Domain.Models.Payment;

namespace WinBind.Application.Abstractions
{
    public interface IPaymentService
    {
        Task<InitializePaymentResponseDto> ProcessPaymentAsync(InitializePaymentCommandRequest request);
        Task<VerifyPaymentResponseDto> VerifyPaymentAsync(VerifyPaymentCommandRequest request);
    }
}
