using Microsoft.EntityFrameworkCore;
using ShoesShop.Application.Common.Interfaces;
using ShoesShop.Application.Common.Models;
using ShoesShop.Domain.Entities.Products;

namespace ShoesShop.Persistance.Repositories
{
	public sealed class TagRepository : Repository<Tag>, ITagRepository
	{
		public TagRepository(ShoesShop_DbContext context) : base(context)
		{

		}

		public async Task<bool> ExistsByNameAsync(string tagTitle, int? excludeId = null, CancellationToken cancellationToken = default)
		{
			var normalizedName = tagTitle.Trim();

			return await DbSet.AsNoTracking().AnyAsync(t => !t.IsDeleted && t.TagTitle == normalizedName && (!excludeId.HasValue || t.ID != excludeId.Value), cancellationToken);
		}

		public async Task<PaginatedResult<Tag>> GetPagedAsync(string? search, int pageNumber, int pageSize, CancellationToken cancellationToken = default)
		{
			IQueryable<Tag> query = DbSet.AsNoTracking().Where(t => !t.IsDeleted);

			if (!string.IsNullOrWhiteSpace(search))
			{
				search = search.Trim();

				query = query.Where(t => t.TagTitle.Contains(search));
			}

			var totalCount = await query.CountAsync(cancellationToken);

			var items = await query.OrderBy(t => t.TagTitle).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);

			return new PaginatedResult<Tag>
			{
				Items = items,
				PageNumber = pageNumber,
				PageSize = pageSize,
				TotalCount = totalCount
			};
		}
	}
}
