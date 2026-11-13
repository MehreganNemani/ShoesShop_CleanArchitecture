using ShoesShop.Api.Contracts.Categories;
using ShoesShop.MVC.Models;
using ShoesShop.MVC.Models.Categories;

namespace ShoesShop.MVC.Services.Interfaces
{
	public interface ICategoryApiClient
	{
		Task<List<CategoryVM>> GetAllAsync(string? search, CancellationToken cancellationToken = default);
		Task<CategoryVM?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
		Task<ApiResult> CreateAsync(CreateCategoryVM model, CancellationToken cancellationToken = default);
		Task<ApiResult> UpdateAsync(int id, UpdateCategoryVM model, CancellationToken cancellationToken = default);
		Task<ApiResult> DeleteAsync(int id, CancellationToken cancellationToken = default);
		Task<IReadOnlyList<CategoryOptionDto>> GetOptionsAsync(CancellationToken cancellationToken = default);
		Task<IReadOnlyList<CategoryOptionDto>> GetAvailableParentsAsync(int categoryId, CancellationToken cancellationToken);
	}
}
