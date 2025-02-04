﻿using WinBind.Domain.Models.Basket;

namespace WinBind.Domain.Models.Order
{
    public class CreateOrderDto
    {
        public Guid? UserId { get; set; }
        public List<CreateBasketItemDto> CreateBasketItemDtos { get; set; }
        public bool IsAuctionProductOrder { get; set; }
        public Guid? AuctionId { get; set; }
    }
}
