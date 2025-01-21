using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinBind.Domain.Models.Category;
using WinBind.Domain.Models.Responses;

namespace WinBind.Application.Features.Queries.Requests
{
    public class GetCategoryByIdQueryRequest : IRequest<ResponseModel<CategoryDto>>
    {
        public Guid Id { get; set; }
        public GetCategoryByIdQueryRequest(Guid ıd)
        {
            Id = ıd;
        }
    }
}
