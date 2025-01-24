using AutoMapper;
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
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                SKU = request.SKU,
                Brand = request.Brand,
                Model = request.Model,
                CaseColor = request.CaseColor,
                CaseShape = request.CaseShape,
                BandColor = request.BandColor,
                DialColor = request.DialColor,
                Gender = request.Gender,
                Technology = request.Technology,                
                IsAvailable = request.IsAvailable,
                UserId = request.UserId,
                CategoryId = request.CategoryId,
                StockCount = request.StockCount,
                IsAuctionProduct = false,
                ProductImages = request.ProductImages.Select(imageDto => new ProductImage
                {
                    Id = Guid.NewGuid(),
                    Path = imageDto.Path,
                    //ProductId = imageDto.ProductId,
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
