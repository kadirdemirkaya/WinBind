using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinBind.Domain.Models.Product;
using WinBind.Domain.Models.Responses;

namespace WinBind.Application.Features.Queries.Requests
{
    public class GetFilteredAndSortedProductsQueryRequest : IRequest<ResponseModel<List<ProductDto>>>
    {
        public string? Brand { get; set; }
        public string? BandColor { get; set; }
        public string? CaseColor { get; set; }
        public string? SortOrder { get; set; }

        public int Page { get; set; }
        public int PageSize { get; set; }
        public GetFilteredAndSortedProductsQueryRequest(int page, int pageSize, string? brand, string? bandColor, string? caseColor, string? sortOrder)
        {
            Page = page;
            PageSize = pageSize;
            Brand = brand;
            BandColor = bandColor;
            CaseColor = caseColor;
            SortOrder = sortOrder;
        }
    }
}
