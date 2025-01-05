using MediatR;
using WinBind.Domain.Models.Responses;
using WinBind.Domain.Models.User;

namespace WinBind.Application.Features.Queries.Requests
{
    public class GetUserByIdQueryRequest : IRequest<ResponseModel<UserModel>>
    {
        public Guid Id { get; set; }

        public GetUserByIdQueryRequest(Guid ıd)
        {
            Id = ıd;
        }
    }
}
