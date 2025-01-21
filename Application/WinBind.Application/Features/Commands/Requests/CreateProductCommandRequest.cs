﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinBind.Domain.Models.Product;
using WinBind.Domain.Models.ProductImage;
using WinBind.Domain.Models.Responses;

namespace WinBind.Application.Features.Commands.Requests
{
    public class CreateProductCommandRequest : IRequest<ResponseModel<bool>>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string SKU { get; set; }
        public bool IsAvailable { get; set; }
        public List<CreateProductImageDto> ProductImages { get; set; }

        public Guid UserId { get; set; }
        public Guid CategoryId { get; set; }
    }
}
