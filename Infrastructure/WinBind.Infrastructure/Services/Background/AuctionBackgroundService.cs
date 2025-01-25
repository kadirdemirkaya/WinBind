using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WinBind.Application.Abstractions;
using WinBind.Application.Features.Commands.Requests;
using WinBind.Application.Features.Queries.Requests;
using WinBind.Domain.Entities;
using WinBind.Domain.Models.Auction;
using WinBind.Domain.Models.Responses;

namespace WinBind.Infrastructure.Services.Background
{
    public class AuctionBackgroundService(IServiceScopeFactory _serviceScopeFactory) : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using (var scope = _serviceScopeFactory.CreateScope())
                    {
                        DateTime currentUtcTime = DateTime.UtcNow;
                        DateTime currentUtcStartTime = new DateTime(currentUtcTime.Year, currentUtcTime.Month, currentUtcTime.Day, currentUtcTime.Hour, currentUtcTime.Minute, 0, DateTimeKind.Utc);
                        DateTime currentUtcEndTime = currentUtcStartTime.AddMinutes(1);

                        IRepository<Auction> _auctionRepo = scope.ServiceProvider.GetRequiredService<IRepository<Auction>>();
                        IMediator _mediatr = scope.ServiceProvider.GetRequiredService<IMediator>();
                        IAuctionService _auctionService = scope.ServiceProvider.GetRequiredService<IAuctionService>();

                        List<Auction> startAuctions = await _auctionRepo.GetAllAsync(a =>
                            a.IsDeleted == false &&
                            a.StartDate >= currentUtcStartTime &&
                            a.StartDate < currentUtcEndTime);

                        List<Auction> endAuctions = await _auctionRepo.GetAllAsync(a =>
                            a.IsDeleted == false &&
                            a.EndDate >= currentUtcStartTime &&
                            a.EndDate < currentUtcEndTime);

                        foreach (var startAuction in startAuctions)
                        {
                            StartAuctionCommandRequest startAuctionCommandRequest = new(startAuction.Id);
                            ResponseModel<bool> responseModel = await _mediatr.Send(startAuctionCommandRequest);
                        }
                        foreach (var endAuction in endAuctions)
                        {
                            EndAuctionCommandRequest endAuctionCommandRequest = new(endAuction.Id);
                            ResponseModel<bool> responseModel = await _mediatr.Send(endAuctionCommandRequest);
                        }

                        GetAllActiveAuctionsQueryRequest getAllActiveAuctionsQueryRequest = new();
                        ResponseModel<List<ActiveAuctionModel>> activeAuctionsResponse = await _mediatr.Send(getAllActiveAuctionsQueryRequest);

                        if (activeAuctionsResponse.Success)
                            await _auctionService.SendAllActiveAuctionsAsync(activeAuctionsResponse.Data);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error occured : {ex.Message}");
                }

                await Task.Delay(TimeSpan.FromSeconds(35), stoppingToken);
            }
        }
    }
}
