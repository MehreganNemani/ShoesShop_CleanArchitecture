using Microsoft.EntityFrameworkCore;
using ShoesShop.Application.Common.Interfaces;
using ShoesShop.Application.Common.Models;
using ShoesShop.Domain.Entities.Products;

namespace ShoesShop.Persistance.Repositories
{
	public sealed class ColorRepository : Repository<Color>, IColorRepository
	{
		public ColorRepository(ShoesShop_DbContext context) : base(context)
		{

		}
		public async Task<bool> ExistsByNameAsync(string colorName, int? excludeId = null, CancellationToken cancellationToken = default)
		{
			var normalizedName = colorName.Trim();

			return await DbSet.AsNoTracking().AnyAsync(c => !c.IsDeleted && c.ColorName == normalizedName && (!excludeId.HasValue || c.ID != excludeId.Value), cancellationToken);
		}

		public async Task<PaginatedResult<Color>> GetPagedAsync(string? search, int pageNumber, int pageSize, CancellationToken cancellationToken = default)
		{
			IQueryable<Color> query = DbSet.AsNoTracking().Where(c => !c.IsDeleted);

			if (!string.IsNullOrWhiteSpace(search))
			{
				search = search.Trim();

				query = query.Where(c => c.ColorName.Contains(search));
			}

			var totalCount = await query.CountAsync(cancellationToken);

			var items = await query.OrderBy(c => c.ColorName).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);

			return new PaginatedResult<Color>
			{
				Items = items,
				PageNumber = pageNumber,
				PageSize = pageSize,
				TotalCount = totalCount
			};
		}
	}
}
