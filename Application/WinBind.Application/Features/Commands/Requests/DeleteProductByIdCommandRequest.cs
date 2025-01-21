using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinBind.Domain.Models.Responses;

namespace WinBind.Application.Features.Commands.Requests
{
    public class DeleteProductByIdCommandRequest : IRequest<ResponseModel<bool>>
    {
        public Guid Id { get; set; }

        public DeleteProductByIdCommandRequest(Guid ıd)
        {
            Id = ıd;
        }
    }
}
