using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WinBind.Application.Abstractions;
using WinBind.Application.Features.Commands.Requests;
using WinBind.Application.Features.Queries.Requests;
using WinBind.Domain.Models.Auction;
using WinBind.Domain.Models.Bid;
using WinBind.Domain.Models.Responses;

namespace WinBind.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AuctionController(IMediator _mediator, ITokenService _tokenService, IHttpContextAccessor _httpContextAccessor) : ControllerBase
    {
        /// <summary>
        /// Müzayede oluşturmak için gerekli bilgiler body de, oluşturulursa eğer true dönecek ve web socket kanalına tüm aktif müzayedeler iletilecek müzayede oluştuğu sırada
        /// </summary>
        /// <param name="createAuctionDto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("create-auction")]
        public async Task<IActionResult> CreateAuction([FromBody] CreateAuctionDto createAuctionDto)
        {
            if (createAuctionDto.UserId is null)
                createAuctionDto.UserId = Guid.Parse(_tokenService.GetClaimFromRequest(_httpContextAccessor.HttpContext, "Id"));

            CreateAuctionCommandRequest createAuctionCommandRequest = new(createAuctionDto);
            ResponseModel<bool> responseModel = await _mediator.Send(createAuctionCommandRequest);

            if (responseModel.Success is false)
                return BadRequest(responseModel);

            return Ok(responseModel);
        }

        /// <summary>
        /// Müzayede durdurmak (ya da sonlandırmak) için gerekli bilgiler header da, sonlandırılırsa eğer true dönecek ve web socket kanalına tüm aktif müzayedeler iletilecek müzayede sonlandırıldığı sırada
        /// </summary>
        /// <param name="auctionId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("end-auction")]
        public async Task<IActionResult> EndAuction([FromHeader] Guid auctionId)
        {
            EndAuctionCommandRequest endAuctionCommandRequest = new(auctionId);
            ResponseModel<bool> responseModel = await _mediator.Send(endAuctionCommandRequest);

            if (responseModel.Success is false)
                return BadRequest(responseModel);

            return Ok(responseModel);
        }

        /// <summary>
        /// UserId'li kişi AuctionId'li müzayedeye vereceği teklif
        /// </summary>
        /// <param name="bidOnAuctionDto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("bid-on-auction")]
        public async Task<IActionResult> BidOnAuction([FromBody] BidOnAuctionDto bidOnAuctionDto)
        {
            BidOnAuctionCommandRequest bidOnAuctionCommandRequest = new(bidOnAuctionDto);
            ResponseModel<bool> responseModel = await _mediator.Send(bidOnAuctionCommandRequest);

            if (responseModel.Success is false)
                return BadRequest(responseModel);

            return Ok(responseModel);
        }

        /// <summary>
        /// Müzayedeleri listeleme yetkilendirme olur çnkü login olmuş kişinin bilgileri lazım
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("get-all-active-auctions")]
        public async Task<IActionResult> GetAllActiveAuctions()
        {
            GetAllActiveAuctionsQueryRequest getAllActiveAuctionsQueryRequest = new();
            ResponseModel<List<ActiveAuctionModel>> responseModel = await _mediator.Send(getAllActiveAuctionsQueryRequest);

            if (responseModel.Success is false)
                return BadRequest(responseModel);

            return Ok(responseModel);
        }
    }
}
