using Iyzipay.Model;
using Iyzipay.Request;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using WinBind.Application.Abstractions;
using WinBind.Application.Features.Commands.Requests;
using WinBind.Domain.Models;
using WinBind.Domain.Models.Payment;

namespace WinBind.Infrastructure.Services
{
    public class IyzicoPaymentService : IPaymentService
    {
        private readonly string apiKey = "sandbox-ea0DfUR4EfJtHGN3J7W4XYJVLryaRTFU";
        private readonly string apiSecret = "sandbox-JuuBsQV3D0HDHXEONqr7srypCpSC1AbE";
        private readonly string _baseUrl = "https://sandbox-api.iyzipay.com";

        Iyzipay.Options options = new Iyzipay.Options()
        {
            ApiKey = "sandbox-ea0DfUR4EfJtHGN3J7W4XYJVLryaRTFU",
            SecretKey = "sandbox-JuuBsQV3D0HDHXEONqr7srypCpSC1AbE",
            BaseUrl = "https://sandbox-api.iyzipay.com"
        };

        public async Task<InitializePaymentResponseDto> ProcessPaymentAsync(InitializePaymentCommandRequest request)
        {
            try
            {
                // Alıcı (buyer) bilgileri
                var buyer = new Buyer
                {
                    Id = request.BuyerId,
                    Name = request.BuyerName,
                    Surname = request.BuyerSurname,
                    Email = request.BuyerEmail,
                    GsmNumber = request.BuyerGsmNumber,
                    IdentityNumber = "11111111111",
                    Ip = "11111111111",
                    //Ip = "85.34.78.112",
                    RegistrationAddress = "ADRES",
                    City = "ADRES",
                    Country = "ADRES",
                    ZipCode = "ADRES"
                };

                var address = new Iyzipay.Model.Address
                {
                    ContactName = buyer.Name + "" + buyer.Surname,
                    ZipCode = "ADRES",
                    Country = "ADRES",
                    City = "ADRES",
                    Description = "DESCRIPTION",
                };

                var basketItem = new List<BasketItem>
            {
                new BasketItem
                {
                    Id = "item1id",
                    Name = "item1name",
                    Category1 = "item1category",
					//Price = request.Price.ToString(),
					Price = request.Price.ToString("F2", CultureInfo.InvariantCulture),
                    ItemType = BasketItemType.PHYSICAL.ToString()

                }
            };

                var checkoutPaymentRequest = new CreateCheckoutFormInitializeRequest()
                {
                    Locale = Locale.TR.ToString(),
                    ConversationId = Guid.NewGuid().ToString(),
                    Currency = Currency.TRY.ToString(),
                    Price = request.Price.ToString("F2", CultureInfo.InvariantCulture),
                    PaidPrice = request.Price.ToString("F2", CultureInfo.InvariantCulture),
                    BasketId = request.BasketId.ToString(),
                    Buyer = buyer,
                    BasketItems = basketItem,
                    BillingAddress = address,
                    ShippingAddress = address,
                    PaymentGroup = PaymentGroup.PRODUCT.ToString(),  // Ürün satın alımı için yapılan ödeme.
                    EnabledInstallments = new List<int> { 1 },      // Taksitsiz işlem
                    CallbackUrl = "http://localhost:5173/tez-frontend/verify-payment",  // Callback URL		
                                                                                          //ForceThreeDS = 1,		
                    PaymentSource = "123123",
                    PosOrderId = "asdasd13",
                };

                var paymentCF = await CheckoutFormInitialize.Create(checkoutPaymentRequest, options);

                if (paymentCF.Status == "success")
                {
                    return new InitializePaymentResponseDto
                    {
                        IsSuccess = true,
                        IyzicoToken = paymentCF.Token,
                        IyzicoCheckoutFormContent = paymentCF.CheckoutFormContent,
                        Message = paymentCF.ErrorMessage,
                    };
                }
                else
                {
                    return new InitializePaymentResponseDto
                    {
                        IsSuccess = false,
                        Message = $"Ödeme işlemi başarısız. Hata : {paymentCF.ErrorGroup} ++++++ {paymentCF.ErrorMessage} ",
                    };
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"ProcessPaymentAsync error : {ex.Message}");
            }

        }

        public async Task<VerifyPaymentResponseDto> VerifyPaymentAsync(VerifyPaymentCommandRequest request)
        {
            try
            {
                var retrieveCFrequest = new RetrieveCheckoutFormRequest
                {
                    Token = request.IyzicoToken,
                };

                var checkoutForm = CheckoutForm.Retrieve(retrieveCFrequest, options);

                if (checkoutForm.Result.PaymentStatus == "SUCCESS")
                {
                    // Ödeme başarılı
                    return new VerifyPaymentResponseDto
                    {
                        //PaymentId = Guid.Parse(checkoutForm.Result.PaymentId),
                        PaymentId = Guid.NewGuid(),
                        OrderId = request.OrderId,
                        PaymentStatus = checkoutForm.Result.PaymentStatus,
                        Amount = checkoutForm.Result.PaidPrice,
                        PaymentDate = DateTime.Now,
                        PaymentMethod = "Credit Card",
                        ErrorMessage = null
                    };
                }
                else
                {
                    // Ödeme başarısız
                    return new VerifyPaymentResponseDto
                    {
                        //PaymentId = Guid.Parse(checkoutForm.Result.PaymentId),
                        PaymentId = Guid.NewGuid(),
                        OrderId = request.OrderId,
                        PaymentStatus = checkoutForm.Result.PaymentStatus,
                        Amount = checkoutForm.Result.PaidPrice,
                        PaymentDate = DateTime.Now,
                        PaymentMethod = "Credit Card",
                        ErrorMessage = checkoutForm.Result.ErrorMessage
                    };
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"VerifyPaymentAsync method: {ex.Message}");
            }

        }
    }
}
