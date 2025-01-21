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
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommandRequest, ResponseModel<bool>>
    {
        private readonly IRepository<Category> _repository;
        private readonly IMapper _mapper;
        public UpdateCategoryCommandHandler(IMapper mapper)
        {
            _mapper = mapper;
        }

        public UpdateCategoryCommandHandler(IRepository<Category> repository)
        {
            _repository = repository;
        }

        public async Task<ResponseModel<bool>> Handle(UpdateCategoryCommandRequest request, CancellationToken cancellationToken)
        {
            var category = await _repository.GetAsync(c => c.Id == request.Id);
            if (category == null)
                return new ResponseModel<bool>("Product is not found", 404);

            var mappedCategory = _mapper.Map<Category>(request);
            var updateStatus = _repository.Update(mappedCategory);
            if (updateStatus is false)
                return new ResponseModel<bool>("Update failed");

            return new ResponseModel<bool>(await _repository.SaveChangesAsync());
        }
    }
}
