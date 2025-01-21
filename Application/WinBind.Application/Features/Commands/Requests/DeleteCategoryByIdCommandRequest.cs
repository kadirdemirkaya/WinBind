using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinBind.Domain.Models.Responses;

namespace WinBind.Application.Features.Commands.Requests
{
    public class DeleteCategoryByIdCommandRequest : IRequest<ResponseModel<bool>>
    {
        public Guid Id { get; set; }
        public DeleteCategoryByIdCommandRequest(Guid ıd)
        {
            Id = ıd;
        }
    }
}
