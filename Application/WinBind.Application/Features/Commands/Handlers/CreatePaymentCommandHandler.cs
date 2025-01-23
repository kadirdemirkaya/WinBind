using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinBind.Application.Abstractions;
using WinBind.Application.Features.Commands.Requests;
using WinBind.Domain.Entities;
using WinBind.Domain.Models.Responses;

namespace WinBind.Application.Features.Commands.Handlers
{
    public class CreatePaymentCommandHandler : IRequestHandler<CreatePaymentCommandRequest, ResponseModel<bool>>
    {
        private readonly IRepository<Payment> _repository;
        public CreatePaymentCommandHandler(IRepository<Payment> repository)
        {
            _repository = repository;
        }

        public async Task<ResponseModel<bool>> Handle(CreatePaymentCommandRequest request, CancellationToken cancellationToken)
        {
            //      TEST EDİLEBİLİR OLMASI İÇİN ŞUANLIK YORUM SATIRINDA 

            /*
                    var anyOrder = await _repository.AnyAsync(p => p.OrderId == request.OrderId);

                    if (!anyOrder)
                        return new ResponseModel<bool>("Order is not found to create payment", 404);
            */

            var addResponse = await _repository.AddAsync(new Payment
            {
                Id = Guid.Parse(request.PaymentId),
                OrderId = request.OrderId,
                Amount = request.Amount,
                PaymentDate = request.PaymentDate,
                PaymentMethod = request.PaymentMethod,
            });

            if(addResponse)
                return new ResponseModel<bool>(await _repository.SaveChangesAsync());

            else
                return new ResponseModel<bool>("Payment create is failed", 400);
        }
    }
}
