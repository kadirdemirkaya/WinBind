using MediatR;
using WinBind.Domain.Models.Order;
using WinBind.Domain.Models.Responses;

namespace WinBind.Application.Features.Queries.Requests
{
    public class GetAllOrdersQueryRequest : IRequest<ResponseModel<List<GetAllOrderModel>>>
    {
        public Guid? UserId { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }

        public GetAllOrdersQueryRequest(Guid? userId, int page, int pageSize)
        {
            UserId = userId;
            Page = page;
            PageSize = pageSize;
        }
    }
}
