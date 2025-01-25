using MediatR;
using WinBind.Application.Abstractions;
using WinBind.Application.Features.Queries.Requests;
using WinBind.Domain.Entities;
using WinBind.Domain.Models.Auction;
using WinBind.Domain.Models.ProductImage;
using WinBind.Domain.Models.Responses;

namespace WinBind.Application.Features.Queries.Handlers
{
    public class GetAllActiveAuctionsQueryHandler(IRepository<Auction> _repository) : IRequestHandler<GetAllActiveAuctionsQueryRequest, ResponseModel<List<ActiveAuctionModel>>>
    {
        public async Task<ResponseModel<List<ActiveAuctionModel>>> Handle(GetAllActiveAuctionsQueryRequest request, CancellationToken cancellationToken)
        {
            List<Auction> auctions = await _repository.GetAllAsync(a => a.IsDeleted == false && a.Product.IsAuctionProduct == true && a.AuctionStatus == Domain.Enums.AuctionStatus.Continues, false, a => a.Product, p => p.Product.ProductImages);

            List<ActiveAuctionModel> activeAuctions = auctions.Select(a => new ActiveAuctionModel
            {
                AuctionId = a.Id,
                EndDate = a.EndDate,
                StartDate = a.StartDate,
                StartingPrice = a.StartingPrice,
                UserId = a.AppUserId,
                ProductDto = new()
                {
                    BandColor = a.Product.BandColor,
                    Brand = a.Product.Brand,
                    CaseColor = a.Product.CaseColor,
                    CaseShape = a.Product.CaseShape,
                    Id = a.ProductId,
                    Name = a.Product.Name,
                    CategoryId = a.Product.CategoryId,
                    Description = a.Product.Description,
                    DialColor = a.Product.DialColor,
                    Gender = a.Product.Gender,
                    IsAvailable = a.Product.IsAvailable,
                    Model = a.Product.Model,
                    Price = a.Product.Price,
                    ProductImages = a.Product.ProductImages.Select(imageDto => new ProductImageDto
                    {
                        Id = Guid.NewGuid(),
                        Path = imageDto.Path,
                        ProductId = imageDto.ProductId,
                    }).ToList(),
                    SKU = a.Product.SKU,
                    StockCount = a.Product.StockCount,
                    Technology = a.Product.Technology,
                    UserId = a.Product.UserId
                }
            }).OrderBy(a => a.StartDate).ToList();

            return new ResponseModel<List<ActiveAuctionModel>>(activeAuctions);
        }
    }
}
