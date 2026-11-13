using ShoesShop.MVC.Models;
using ShoesShop.MVC.Models.Color;

namespace ShoesShop.MVC.Services.Interfaces
{
    public interface IColorApiClient
    {
        Task<PaginatedViewModel<ColorVM>> GetPagedAsync(string? search, int pageNumber, int pageSize, CancellationToken cancellationToken = default);
        Task<ColorVM?> GetByIdAsync(int id,CancellationToken cancellationToken = default);
        Task<ApiResult> CreateAsync(CreateColorVM model, CancellationToken cancellationToken = default);
        Task<ApiResult> UpdateAsync(int id, EditColorVM model, CancellationToken cancellationToken = default);
        Task<ApiResult> DeleteAsync(int id, CancellationToken cancellationToken = default);

        Task<List<ColorVM>> GetAllAsync();
    }
}
