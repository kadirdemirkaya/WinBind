using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinBind.Domain.Models.Payment
{
    public class InitializePaymentResponseDto
    {
        public string IyzicoToken { get; set; }
        public string IyzicoCheckoutFormContent { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
