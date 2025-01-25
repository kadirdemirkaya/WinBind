using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WinBind.Application.Abstractions;
using WinBind.Application.Features.Commands.Requests;
using WinBind.Domain.Entities;
using WinBind.Domain.Entities.Identity;
using WinBind.Domain.Models.Responses;

namespace WinBind.Application.Features.Commands.Handlers
{
    public class CreateAuctionCommandHandler(IRepository<Auction> _auctionRepo, IRepository<Product> _productRepo, UserManager<AppUser> _userManager) : IRequestHandler<CreateAuctionCommandRequest, ResponseModel<bool>>
    {
        public async Task<ResponseModel<bool>> Handle(CreateAuctionCommandRequest request, CancellationToken cancellationToken)
        {
            Product product = new()
            {
                Id = Guid.NewGuid(),
                BandColor = request.CreateAuctionDto.BandColor,
                CaseColor = request.CreateAuctionDto.CaseColor,
                CaseShape = request.CreateAuctionDto.CaseShape,
                DialColor = request.CreateAuctionDto.DialColor,
                Description = request.CreateAuctionDto.Description,
                Gender = request.CreateAuctionDto.Gender,
                IsAuctionProduct = true,
                IsDeleted = false,
                Model = request.CreateAuctionDto.Model,
                Name = request.CreateAuctionDto.Name,
                Price = request.CreateAuctionDto.Price,
                SKU = request.CreateAuctionDto.SKU,
                Technology = request.CreateAuctionDto.Technology,
                UserId = request.CreateAuctionDto.UserId ?? Guid.Empty,
                CategoryId = request.CreateAuctionDto.CategoryId,
                Brand = request.CreateAuctionDto.Brand,
                StockCount = request.CreateAuctionDto.StockCount,
                CreatedAtUtc = DateTime.UtcNow,
                ProductImages = request.CreateAuctionDto.ProductImages.Select(imageDto => new ProductImage
                {
                    Id = Guid.NewGuid(),
                    Path = imageDto.Path,
                    CreatedAtUtc = DateTime.UtcNow,
                    IsDeleted = false,
                }).ToList(),
                IsAvailable = true,
            };

            if (await _productRepo.AddAsync(product))
            {
                if (await _productRepo.SaveChangesAsync())
                {
                    Auction auction = new()
                    {
                        Id = Guid.NewGuid(),
                        AppUserId = request.CreateAuctionDto.UserId ?? Guid.Empty,
                        Count = request.CreateAuctionDto.StockCount,
                        StartDate = request.CreateAuctionDto.StartDate,
                        EndDate = request.CreateAuctionDto.EndDate,
                        ProductId = product.Id,
                        CreatedAtUtc = DateTime.UtcNow,
                        IsDeleted = false,
                        AuctionStatus = Domain.Enums.AuctionStatus.NotStart,
                        StartingPrice = request.CreateAuctionDto.StartingPrice,
                    };

                    if (await _auctionRepo.AddAsync(auction))
                        if (await _auctionRepo.SaveChangesAsync())
                            return new ResponseModel<bool>(true);
                        else
                            return new ResponseModel<bool>("Auction could not be created", 400);
                }
                return new ResponseModel<bool>("Product not created for auction", 400);
            }
            return new ResponseModel<bool>("Product not added for auction", 400);
        }
    }
}
