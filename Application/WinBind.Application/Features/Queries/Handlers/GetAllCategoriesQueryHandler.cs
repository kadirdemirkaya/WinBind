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
using WinBind.Domain.Models.Product;
using WinBind.Domain.Models.Responses;

namespace WinBind.Application.Features.Queries.Handlers
{
    public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQueryRequest, ResponseModel<List<CategoryDto>>>
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Category> _repository;
        public GetAllCategoriesQueryHandler(IRepository<Category> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ResponseModel<List<CategoryDto>>> Handle(GetAllCategoriesQueryRequest request, CancellationToken cancellationToken)
        {
            var categories = await _repository.GetAllAsync();

            var mappedCategories = _mapper.Map<List<CategoryDto>>(categories);

            return new ResponseModel<List<CategoryDto>>(mappedCategories);
        }
    }
}
