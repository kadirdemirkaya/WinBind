using MediatR;
using Microsoft.AspNetCore.Components;
using WinBind.Application.Abstractions;
using WinBind.Application.Features.Commands.Requests;
using WinBind.Domain.Entities;
using WinBind.Domain.Models.Responses;

namespace WinBind.Application.Features.Commands.Handlers
{
    public class DeleteOrderCommandHandler(IRepository<Order> _repository) : IRequestHandler<DeleteOrderCommandRequest, ResponseModel<bool>>
    {
        public async Task<ResponseModel<bool>> Handle(DeleteOrderCommandRequest request, CancellationToken cancellationToken)
        {
            Order? order = await _repository.GetAsync(o => o.Id == request.OrderId && o.IsDeleted == false);

            if(order is not null)
            {
                order.IsDeleted = true;
                order.UpdatedAtUtc = DateTime.UtcNow;

                bool saveResponse = await _repository.SaveChangesAsync();

                return saveResponse is true ? new ResponseModel<bool>(true) : new ResponseModel<bool>("Order could not be deleted", 400);
            }

            return new ResponseModel<bool>("Order is not found",400);
        }
    }
}
