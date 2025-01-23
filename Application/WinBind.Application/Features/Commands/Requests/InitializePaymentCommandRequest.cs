using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinBind.Domain.Models.Payment;
using WinBind.Domain.Models.Responses;

namespace WinBind.Application.Features.Commands.Requests
{
    public class InitializePaymentCommandRequest : IRequest<InitializePaymentResponseDto>
    {
        // Kullanıcı (Buyer) Bilgileri
        [Required]
        public string BuyerId { get; set; }                     // +
        [Required]
        public string BuyerName { get; set; }                   // +
        [Required]
        public string BuyerSurname { get; set; }                // +
        [Required]
        public string BuyerEmail { get; set; }                  // +
        [Required]
        public string BuyerGsmNumber { get; set; }              // +


        // Ödeme Detayları                                      
        [Required]
        public string OrderId { get; set; }
        [Required]
        public decimal Price { get; set; }                      // +

        // Sepet Bilgileri
        [Required]
        public string BasketId { get; set; }                    // + 
    }
}
