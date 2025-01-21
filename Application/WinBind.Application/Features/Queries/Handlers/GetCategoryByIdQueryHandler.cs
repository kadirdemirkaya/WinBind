using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinBind.Application.Abstractions;
using WinBind.Application.Features.Queries.Requests;
using WinBind.Domain.Entities;
using WinBind.Domain.Models.Category;
using WinBind.Domain.Models.Responses;

namespace WinBind.Application.Features.Queries.Handlers
{
    public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQueryRequest, ResponseModel<CategoryDto>>
    {
        private readonly IRepository<Category> _repository;
        private readonly IMapper _mapper;
        public GetCategoryByIdQueryHandler(IMapper mapper, IRepository<Category> repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<ResponseModel<CategoryDto>> Handle(GetCategoryByIdQueryRequest request, CancellationToken cancellationToken)
        {
            var category = await _repository.GetAsync(c => c.Id == request.Id);
            
            if (category == null)
                return new ResponseModel<CategoryDto>("Category is not found",404);

            var mappedCategory = _mapper.Map<CategoryDto>(category);
            return new ResponseModel<CategoryDto>(mappedCategory);
        }
    }
}
