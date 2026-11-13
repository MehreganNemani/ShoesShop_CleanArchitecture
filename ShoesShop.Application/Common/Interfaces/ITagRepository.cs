using ShoesShop.Application.Common.Models;
using ShoesShop.Domain.Entities.Products;

namespace ShoesShop.Application.Common.Interfaces
{
	public interface ITagRepository : IRepository<Tag>
	{
		Task<PaginatedResult<Tag>> GetPagedAsync(string? search, int pageNumber, int pageSize, CancellationToken cancellationToken = default);
		Task<bool> ExistsByNameAsync(string tagTitle, int? excludeId = null, CancellationToken cancellationToken = default);
	}
}
