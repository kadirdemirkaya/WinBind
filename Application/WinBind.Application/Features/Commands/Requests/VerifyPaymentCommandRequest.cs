using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinBind.Domain.Models.Payment;

namespace WinBind.Application.Features.Commands.Requests
{
    public class VerifyPaymentCommandRequest : IRequest<VerifyPaymentResponseDto>
    {
        [Required]
        public string OrderId { get; set; }
        [Required]
        public string IyzicoToken { get; set; }
    }
}
