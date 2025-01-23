using AutoMapper;
using WinBind.Domain.Entities;
using WinBind.Domain.Models.Auction;

namespace WinBind.Application.Mappers.Auctions
{
    public class AuctionMap : Profile
    {
        public AuctionMap()
        {
            CreateMap<Auction, CreateAuctionDto>().ReverseMap();
        }
    }
}
