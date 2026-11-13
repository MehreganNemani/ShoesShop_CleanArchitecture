using ShoesShop.MVC.Models;
using ShoesShop.MVC.Models.Tags;

namespace ShoesShop.MVC.Services.Interfaces
{
	public interface ITagApiClient
	{
		Task<PaginatedViewModel<TagVM>> GetPagedAsync(string? search, int pageNumber, int pageSize, CancellationToken cancellationToken = default);
		Task<TagVM?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
		Task<ApiResult> CreateAsync(CreateTagVM model, CancellationToken cancellationToken = default);
		Task<ApiResult> UpdateAsync(int id, EditTagVM model, CancellationToken cancellationToken = default);
		Task<ApiResult> DeleteAsync(int id, CancellationToken cancellationToken = default);
		Task<List<TagVM>> GetAllAsync();
	}
}
