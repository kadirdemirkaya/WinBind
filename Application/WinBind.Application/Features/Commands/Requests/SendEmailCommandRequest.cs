using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinBind.Domain.Enums;
using WinBind.Domain.Models.Responses;

namespace WinBind.Application.Features.Commands.Requests
{
    public class SendEmailCommandRequest : IRequest<ResponseModel<bool>>
    {
        [Required]
        public string ToEmail { get; set; }
        public string Subject { get; set; }
        [Required]
        public string Body { get; set; }
        [Required]
        public EmailType EmailType { get; set; }

        // Email ise
        public string? AuctionProductName { get; set; }

        // Kayıt ise
        public string? UserFirstName { get; set; }

        // Satın alım ise
        public string? PurchasedProductName { get; set; }
    }
}
