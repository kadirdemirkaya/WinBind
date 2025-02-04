﻿using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WinBind.Application.Abstractions;
using WinBind.Application.Features.Commands.Requests;
using WinBind.Domain.Entities;
using WinBind.Domain.Models.Order;
using WinBind.Domain.Models.Responses;

namespace WinBind.Application.Features.Commands.Handlers
{
    public class CreateOrderCommandHandler(IRepository<Basket> _basketRepository, IRepository<Order> _orderRepository, IRepository<Product> _productRepository, IRepository<Auction> _auctionRepository, IMapper _mapper) : IRequestHandler<CreateOrderCommandRequest, ResponseModel<CreateOrderModel>>
    {
        public async Task<ResponseModel<CreateOrderModel>> Handle(CreateOrderCommandRequest request, CancellationToken cancellationToken)
        {
            bool anyActiveBasket = await _basketRepository.AnyAsync(b => b.UserId == request.CreateOrderDto.UserId && b.IsDeleted == false);

            if (!anyActiveBasket)
            {
                Basket basket = new()
                {
                    Id = Guid.NewGuid(),
                    UserId = request.CreateOrderDto.UserId ?? Guid.Empty,
                    BasketItems = _mapper.Map<List<BasketItem>>(request.CreateOrderDto.CreateBasketItemDtos),
                    IsDeleted = true
                };

                await _basketRepository.AddAsync(basket);
                bool saveResponse = await _basketRepository.SaveChangesAsync();
                if (saveResponse)
                {
                    decimal totalAmount = 0;
                    int i = 0;

                    foreach (var basketItemDto in request.CreateOrderDto.CreateBasketItemDtos)
                    {
                        Product product = await _productRepository.GetAsync(p => p.Id == basketItemDto.ProductId && p.IsDeleted == false);

                        Auction auction = await _auctionRepository.Table.Where(a => a.Id == request.CreateOrderDto.AuctionId).Include(a => a.AuctionResult).ThenInclude(ar => ar.WinningBidDetails).FirstOrDefaultAsync();

                        if (request.CreateOrderDto.IsAuctionProductOrder)
                        {
                            totalAmount += auction.AuctionResult.FinalPrice * basketItemDto.Quantity;
                        }
                        else
                        {
                            totalAmount += product.Price * basketItemDto.Quantity;
                        }

                        if (i == 0)
                        {
                            if (product.StockCount >= basketItemDto.Quantity)
                            {
                                product.StockCount -= basketItemDto.Quantity;
                                _productRepository.Update(product);
                                await _productRepository.SaveChangesAsync();
                            }
                            else
                            {
                                return new ResponseModel<CreateOrderModel>(new CreateOrderModel()
                                {
                                    IsSuccess = false,
                                    OrderId = Guid.Empty
                                });
                            }

                            i += 1;
                        }
                    }

                    Order order = new()
                    {
                        Id = Guid.NewGuid(),
                        OrderDate = DateTime.UtcNow,
                        TotalAmount = totalAmount,
                        UserId = request.CreateOrderDto.UserId ?? Guid.Empty,
                        Status = "Order",
                        BasketId = basket.Id
                    };

                    await _orderRepository.AddAsync(order);
                    saveResponse = await _orderRepository.SaveChangesAsync();

                    CreateOrderModel createOrderModel = new()
                    {
                        IsSuccess = true,
                        OrderId = order.Id
                    };

                    return saveResponse is true ? new ResponseModel<CreateOrderModel>(createOrderModel) : new ResponseModel<CreateOrderModel>("Order could not be created", 400);
                }
                return new ResponseModel<CreateOrderModel>("A error occured while order created", 400);
            }
            return new ResponseModel<CreateOrderModel>("already exists active basket", 400);
        }
    }
}
