﻿using WinBind.Domain.Entities.Base;

namespace WinBind.Domain.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string SKU { get; set; }
        public bool IsAvailable { get; set; }

        public Guid CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
        public virtual ICollection<BasketItem> BasketItems { get; set; }
        public virtual ICollection<Auction> Auctions { get; set; }

        public virtual ICollection<ProductImage> ProductImages { get; set; }
    }
}
