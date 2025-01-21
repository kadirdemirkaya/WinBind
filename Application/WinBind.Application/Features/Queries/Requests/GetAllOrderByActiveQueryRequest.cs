using MediatR;
using WinBind.Domain.Models.Order;
using WinBind.Domain.Models.Responses;

namespace WinBind.Application.Features.Queries.Requests
{
    public class GetAllOrderByActiveQueryRequest : IRequest<ResponseModel<List<GetAllOrderModel>>>
    {
        public Guid? UserId { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public bool IsDeletd { get; set; }
        public GetAllOrderByActiveQueryRequest(Guid? userId, int page, int pageSize, bool ısDeletd)
        {
            UserId = userId;
            Page = page;
            PageSize = pageSize;
            IsDeletd = ısDeletd;
        }
    }
}
