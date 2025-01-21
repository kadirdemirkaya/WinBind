using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinBind.Domain.Models.Category;
using WinBind.Domain.Models.Responses;

namespace WinBind.Application.Features.Commands.Requests
{
    public class CreateCategoryCommandRequest : IRequest<ResponseModel<bool>>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
