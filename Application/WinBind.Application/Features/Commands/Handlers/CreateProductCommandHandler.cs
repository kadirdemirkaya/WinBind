﻿using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinBind.Application.Abstractions;
using WinBind.Application.Features.Commands.Requests;
using WinBind.Domain.Entities;
using WinBind.Domain.Models.Responses;

namespace WinBind.Application.Features.Commands.Handlers
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommandRequest, ResponseModel<bool>>
    {
        private readonly IRepository<Product> _repository;
        private readonly IMapper _mapper;
        public CreateProductCommandHandler(IRepository<Product> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ResponseModel<bool>> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
        {
            bool addResponse = await _repository.AddAsync(new Product()
            {
                Id = Guid.NewGuid(),
                Name = request.ProductDto.Name,
                Description = request.ProductDto.Description,
                Price = request.ProductDto.Price,
                SKU = request.ProductDto.SKU,
                IsAvailable = request.ProductDto.IsAvailable,
                UserId = request.ProductDto.UserId,
                CategoryId = request.ProductDto.CategoryId,
                ProductImages = request.ProductImagesDto.Select(imageDto => new ProductImage
                {
                    Id = Guid.NewGuid(),
                    Path = imageDto.Path,
                    ProductId = imageDto.ProductId,
                    CreatedAtUtc = DateTime.UtcNow,
                    IsDeleted = false,
                }).ToList()

            });

            if (addResponse is false) 
                return new ResponseModel<bool>("Product could not be created", 400);

            return new ResponseModel<bool>(await _repository.SaveChangesAsync());
        }
    }
}
