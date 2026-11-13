using ShoesShop.Application.Common.Models;
using ShoesShop.Domain.Entities.Products;

namespace ShoesShop.Application.Common.Interfaces
{
	public interface IColorRepository : IRepository<Color>
	{
		Task<PaginatedResult<Color>> GetPagedAsync(string? search, int pageNumber, int pageSize, CancellationToken cancellationToken = default);
		Task<bool> ExistsByNameAsync(string colorName, int? excludeId = null, CancellationToken cancellationToken = default);
	}
}
