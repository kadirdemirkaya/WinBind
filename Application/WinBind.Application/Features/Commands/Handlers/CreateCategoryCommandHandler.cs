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
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommandRequest, ResponseModel<bool>>
    {
        private readonly IRepository<Category> _repository;
        public CreateCategoryCommandHandler(IRepository<Category> repository)
        {
            _repository = repository;
        }

        public async Task<ResponseModel<bool>> Handle(CreateCategoryCommandRequest request, CancellationToken cancellationToken)
        {
            var addStatus = await _repository.AddAsync(new Category
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Description = request.Description,
                CreatedAtUtc = DateTime.UtcNow,
            });

            if (addStatus is false)
                return new ResponseModel<bool>("Add action is failed");

            return new ResponseModel<bool>(await _repository.SaveChangesAsync());
        }
    }
}
