using AutoMapper;
using MediatR;
using WinBind.Application.Abstractions;
using WinBind.Application.Features.Commands.Requests;
using WinBind.Domain.Entities;
using WinBind.Domain.Models.Responses;

namespace WinBind.Application.Features.Commands.Handlers
{
    public class DeleteProductByIdCommandHandler : IRequestHandler<DeleteProductByIdCommandRequest, ResponseModel<bool>>
    {
        private readonly IRepository<Product> _repository;
        private readonly IMapper _mapper;
        public DeleteProductByIdCommandHandler(IRepository<Product> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ResponseModel<bool>> Handle(DeleteProductByIdCommandRequest request, CancellationToken cancellationToken)
        {
            var product = await _repository.GetAsync(p => p.Id == request.Id);

            if (product is not null)
            {
                product.IsDeleted = true;

                bool updateStatus = _repository.Update(product);
                if (updateStatus)
                    return new ResponseModel<bool>(await _repository.SaveChangesAsync());

                return new ResponseModel<bool>("Delete failed", 400);
            }
            return new ResponseModel<bool>("Product not found", 404);
        }
    }
}
