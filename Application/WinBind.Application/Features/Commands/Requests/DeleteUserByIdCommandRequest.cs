using MediatR;
using WinBind.Domain.Models.Responses;

namespace WinBind.Application.Features.Commands.Requests
{
    public class DeleteUserByIdCommandRequest : IRequest<ResponseModel<bool>>
    {
        public Guid Id { get; set; }

        public DeleteUserByIdCommandRequest(Guid ıd)
        {
            Id = ıd;
        }
    }
}
