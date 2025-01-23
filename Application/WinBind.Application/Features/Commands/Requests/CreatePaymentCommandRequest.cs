using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinBind.Domain.Models.Responses;

namespace WinBind.Application.Features.Commands.Requests
{
    public class CreatePaymentCommandRequest : IRequest<ResponseModel<bool>>
    {
        [Required]
        public string PaymentId { get; set; }   // Iyzico payment servisinde paymentId önceden üretiliyor
        [Required]
        public Guid OrderId { get; set; }
        [Required]
        public DateTime PaymentDate { get; set; }
        [Required]
        public decimal Amount { get; set; }
        [Required]
        public string PaymentMethod { get; set; }
    }
}
