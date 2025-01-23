using AutoMapper;
using MediatR;
using WinBind.Application.Abstractions;
using WinBind.Application.Features.Commands.Requests;
using WinBind.Domain.Entities;
using WinBind.Domain.Models.Responses;

namespace WinBind.Application.Features.Commands.Handlers
{
    public class CreateAuctionCommandHandler(IRepository<Auction> _repository, IMapper _mapper) : IRequestHandler<CreateAuctionCommandRequest, ResponseModel<bool>>
    {
        public async Task<ResponseModel<bool>> Handle(CreateAuctionCommandRequest request, CancellationToken cancellationToken)
        {
            bool existAuction = await _repository.AnyAsync(a => a.ProductId == request.CreateAuctionDto.ProductId && a.IsDeleted == false);

            if (existAuction is false)
            {
                Auction newAuction = _mapper.Map<Auction>(request.CreateAuctionDto);

                await _repository.AddAsync(newAuction);

                bool saveResponse = await _repository.SaveChangesAsync();

                if (saveResponse)
                {
                    // Bu kısımda tüm aktif müzayedeleri web sockete aktarmamız lazım.
                }

                return saveResponse is true ? new ResponseModel<bool>() : new ResponseModel<bool>("Auction could not be created", 400);
            }

            return new ResponseModel<bool>("Auction already exists", 400);
        }
    }
}
