using ShoesShop.MVC.Models;
using ShoesShop.MVC.Models.Products;

namespace ShoesShop.MVC.Services.Interfaces
{
	public interface IProductApiClient
	{
		Task<PaginatedViewModel<ProductVM>> GetPagedAsync(string? search, int pageNumber, int pageSize, CancellationToken cancellationToken = default);
		Task<ProductVM?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
		Task<ApiResult> CreateAsync(CreateProductVM model, CancellationToken cancellationToken = default);
		Task<ApiResult> UpdateAsync(int id, EditProductVM model, CancellationToken cancellationToken = default);
		Task<ApiResult> DeleteAsync(int id, CancellationToken cancellationToken = default);
		Task<ProductInfoDto?> GetByIdAsync(int productId);
	}
}
