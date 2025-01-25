using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinBind.Domain.Models.Auction;
using WinBind.Domain.Models.Responses;

namespace WinBind.Application.Features.Queries.Requests
{
    public class GetAuctionsByUserQueryRequest : IRequest<ResponseModel<List<GetAuctionsByUserIdDto>>>
    { 
        public Guid Id { get; set; }
        public GetAuctionsByUserQueryRequest(Guid ıd)
        {
            Id = ıd;
        }
    }
}
