using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinBind.Domain.Models.Category;
using WinBind.Domain.Models.Order;
using WinBind.Domain.Models.Responses;

namespace WinBind.Application.Features.Queries.Requests
{
    public class GetAllCategoriesQueryRequest : IRequest<ResponseModel<List<CategoryDto>>>
    {
    }
}
