using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinBind.Domain.Models.Product;
using WinBind.Domain.Models.Responses;

namespace WinBind.Application.Features.Queries.Requests
{
    public class GetProductByIdQueryRequest : IRequest<ResponseModel<ProductDto>>
    {
        public Guid Id { get; set; } 
        public GetProductByIdQueryRequest(Guid ıd)
        {
            Id = ıd;
        }
    }
}
