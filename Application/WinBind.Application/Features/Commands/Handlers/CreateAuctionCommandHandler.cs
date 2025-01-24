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
    public class CreateAuctionCommandHandler(IRepository<Auction> _repository, UserManager<AppUser> _userManager) : IRequestHandler<CreateAuctionCommandRequest, ResponseModel<bool>>
    {
        public async Task<ResponseModel<bool>> Handle(CreateAuctionCommandRequest request, CancellationToken cancellationToken)
        {
            bool existAuction = await _repository.AnyAsync(a => a.ProductId == request.CreateAuctionDto.ProductId && a.IsDeleted == false, false);

            if (existAuction is false)
            {
                AppUser? appUser = await _userManager.Users.Where(u => u.Id == request.CreateAuctionDto.UserId).FirstOrDefaultAsync();

                Auction newAuction = new Auction()
                {
                    Id = Guid.NewGuid(),
                    Count = request.CreateAuctionDto.Count,
                    ProductId = request.CreateAuctionDto.ProductId,
                    CreatedAtUtc = DateTime.UtcNow,
                    StartDate = request.CreateAuctionDto.StartDate,
                    EndDate = request.CreateAuctionDto.EndDate,
                    AppUserId = request.CreateAuctionDto.UserId ?? Guid.Empty,
                    StartingPrice = request.CreateAuctionDto.StartingPrice,
                };

                await _repository.AddAsync(newAuction);

                bool saveResponse = await _repository.SaveChangesAsync();

                if (saveResponse)
                {
                    // Bu kısımda tüm aktif müzayedeleri web sockete aktarmamız lazım.

                    return new ResponseModel<bool>(true, 200);
                }

                return saveResponse is true ? new ResponseModel<bool>(true, 200) : new ResponseModel<bool>("Auction could not be created", 400);
            }
            return new ResponseModel<bool>("Auction already exists", 400);
        }
    }
}
