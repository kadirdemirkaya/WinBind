using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinBind.Application.Helpers
{
    public static class EmailMessageBuilder 
    {
        public static string BuildRegistrationEmail(string userName)
        {
            return $"<h1>Hoşgeldin, {userName}!</h1><p>Kayıt olduğunuz için teşekkürler. İyi alışverişler dileriz. </p>";
        }
        public static string BuildAuctionWinEmail(string userName, string itemName)
        {
            return $"<h1>Tebrikler {userName}!</h1><p>{itemName} müzayedesini kazandınız.</p>";
        }

        public static string BuildPurchaseEmail(string userName, string productName)
        {
            return $"<h1>Teşekkürler {userName}!</h1><p>{productName} ürününüz başarıyla satın alındı.</p>";
        }
    }
}
