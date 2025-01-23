using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinBind.Domain.Models.Payment
{
    public class VerifyPaymentResponseDto
    {
        public string PaymentId { get; set; }
        public string OrderId { get; set; }
        public DateTime PaymentDate { get; set; }
        public string Amount { get; set; }
        public string PaymentMethod { get; set; }
        public string PaymentStatus { get; set; }
        public string ErrorMessage { get; set; }
    }
}
