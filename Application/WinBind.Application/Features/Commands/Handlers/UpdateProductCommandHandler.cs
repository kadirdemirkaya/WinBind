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
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommandRequest, ResponseModel<bool>>
    {
        private readonly IRepository<Product> _repository;
        private readonly IMapper _mapper;
        public UpdateProductCommandHandler(IMapper mapper, IRepository<Product> repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<ResponseModel<bool>> Handle(UpdateProductCommandRequest request, CancellationToken cancellationToken)
        {
            var product = await _repository.GetAsync(p => p.Id == request.ProductDto.Id);

            if (product == null)
                return new ResponseModel<bool>("Product is not found", 404);

            var mappedProduct = _mapper.Map<Product>(request);
            var updateStatus = _repository.Update(mappedProduct);

            if (updateStatus is false)
                return new ResponseModel<bool>("Update failed", 400);

            return new ResponseModel<bool>(await _repository.SaveChangesAsync());
        }
    }
}
