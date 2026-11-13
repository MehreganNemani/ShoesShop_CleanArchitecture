using ShoesShop.Application.Common.Models;
using ShoesShop.Domain.Entities.Products;

namespace ShoesShop.Application.Common.Interfaces
{
	public interface IProductRepository : IRepository<Product>
	{
		Task<PaginatedResult<Product>> GetPagedAsync(string? search, int pageNumber, int pageSize, CancellationToken cancellationToken = default);
		Task<bool> ExistsByNameAsync(string productName, int? excludeId = null, CancellationToken cancellationToken = default);
		Task<bool> ExistsByCodeAsync(string productCode, int? excludeId = null, CancellationToken cancellationToken = default);
		Task<bool> ExistsByModelAsync(string productModel, int? excludeId = null, CancellationToken cancellationToken = default);
	}
}
