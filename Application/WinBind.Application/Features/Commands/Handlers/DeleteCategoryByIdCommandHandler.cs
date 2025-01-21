using MediatR;
using WinBind.Application.Abstractions;
using WinBind.Application.Features.Commands.Requests;
using WinBind.Domain.Entities;
using WinBind.Domain.Models.Responses;

namespace WinBind.Application.Features.Commands.Handlers
{
    public class DeleteCategoryByIdCommandHandler : IRequestHandler<DeleteCategoryByIdCommandRequest, ResponseModel<bool>>
    {
        private readonly IRepository<Category> _repository;
        public DeleteCategoryByIdCommandHandler(IRepository<Category> repository)
        {
            _repository = repository;
        }

        public async Task<ResponseModel<bool>> Handle(DeleteCategoryByIdCommandRequest request, CancellationToken cancellationToken)
        {
            var category = await _repository.GetAsync(p => p.Id == request.Id);

            if (category is not null)
            {
                category.IsDeleted = true;

                bool updateStatus = _repository.Update(category);
                if (updateStatus)
                    return new ResponseModel<bool>(await _repository.SaveChangesAsync());

                return new ResponseModel<bool>("Delete failed", 400);
            }
            return new ResponseModel<bool>("Category not found", 404);
        }
    }
}
