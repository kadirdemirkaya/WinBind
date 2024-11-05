using WinBind.Domain.Models.Options;

namespace WinBind.Application.Abstractions
{
    public interface IPaginationService
    {
        PaginationOptions GetPaginationOptions();
    }
}
