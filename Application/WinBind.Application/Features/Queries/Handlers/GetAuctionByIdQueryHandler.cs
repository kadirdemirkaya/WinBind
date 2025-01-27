using MediatR;
using WinBind.Application.Abstractions;
using WinBind.Application.Features.Queries.Requests;
using WinBind.Domain.Entities;
using WinBind.Domain.Models.Auction;
using WinBind.Domain.Models.ProductImage;
using WinBind.Domain.Models.Responses;

namespace WinBind.Application.Features.Queries.Handlers
{
    public class GetAuctionByIdQueryHandler(IRepository<Auction> _repository) : IRequestHandler<GetAuctionByIdQueryRequest, ResponseModel<ActiveAuctionModel>>
    {
        public async Task<ResponseModel<ActiveAuctionModel>> Handle(GetAuctionByIdQueryRequest request, CancellationToken cancellationToken)
        {
            Auction? auction = await _repository.GetAsync(
                 a => a.IsDeleted == false &&
                 a.Id == request.AuctionId,
                 false,
                 a => a.Product,
                 p => p.Product.ProductImages,
                 a => a.Bids);

            ActiveAuctionModel activeAuction = new ActiveAuctionModel
            {
                AuctionId = auction.Id,
                EndDate = auction.EndDate,
                StartDate = auction.StartDate,
                StartingPrice = auction.StartingPrice,
                UserId = auction.AppUserId,
                CurrentPrice = auction.Bids.Count > 0 ? auction.Bids.Max(b => b.BidAmount) : auction.StartingPrice,
                ProductDto = new()
                {
                    BandColor = auction.Product.BandColor,
                    Brand = auction.Product.Brand,
                    CaseColor = auction.Product.CaseColor,
                    CaseShape = auction.Product.CaseShape,
                    Id = auction.ProductId,
                    Name = auction.Product.Name,
                    CategoryId = auction.Product.CategoryId,
                    Description = auction.Product.Description,
                    DialColor = auction.Product.DialColor,
                    Gender = auction.Product.Gender,
                    IsAvailable = auction.Product.IsAvailable,
                    Model = auction.Product.Model,
                    Price = auction.Product.Price,
                    ProductImages = auction.Product.ProductImages.Select(imageDto => new ProductImageDto
                    {
                        Id = Guid.NewGuid(), // Yeni bir ID oluşturuyorsunuz, bu doğru mu kontrol edin.
                        Path = imageDto.Path,
                        ProductId = imageDto.ProductId,
                    }).ToList(),
                    SKU = auction.Product.SKU,
                    StockCount = auction.Product.StockCount,
                    Technology = auction.Product.Technology,
                    UserId = auction.Product.UserId
                }
            };

            return new ResponseModel<ActiveAuctionModel>(activeAuction);
        }
    }
}
