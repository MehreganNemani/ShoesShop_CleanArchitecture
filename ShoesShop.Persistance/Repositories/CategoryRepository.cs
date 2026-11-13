using Microsoft.EntityFrameworkCore;
using ShoesShop.Application.Common.Interfaces;
using ShoesShop.Domain.Entities.Products;

namespace ShoesShop.Persistance.Repositories
{
	public sealed class CategoryRepository : Repository<Category>, ICategoryRepository
	{
		public CategoryRepository(ShoesShop_DbContext context) : base(context)
		{

		}
		public async Task<bool> IsExistAsync(int id, CancellationToken cancellationToken = default)
		{
			return await DbSet.AnyAsync(c => c.ID == id && !c.IsDeleted, cancellationToken);
		}

		public async Task<bool> IsNameExistAsync(string name, int? excludeId = null, CancellationToken cancellationToken = default)
		{
			var normalizedName = name.Trim();

			var query = DbSet.Where(c => !c.IsDeleted && c.CategoryName == normalizedName);

			if (excludeId.HasValue)
			{
				query = query.Where(c => c.ID != excludeId.Value);
			}

			return await query.AnyAsync(cancellationToken);
		}

		public async Task<bool> IsCodeExistAsync(int code, int? excludeId = null, CancellationToken cancellationToken = default)
		{
			var query = DbSet.Where(c => !c.IsDeleted && c.Code == code);

			if (excludeId.HasValue)
			{
				query = query.Where(c => c.ID != excludeId.Value);
			}

			return await query.AnyAsync(cancellationToken);

		}
		public async Task<bool> ParentExistsAsync(int parentId, CancellationToken cancellationToken = default)
		{
			return await DbSet.AnyAsync(c => c.ID == parentId && !c.IsDeleted, cancellationToken);
		}

		public async Task<bool> IsChildOfAsync(int categoryId, int potentialParentId, CancellationToken cancellationToken = default)
		{
			var currentParentId = await DbSet.Where(c => c.ID == potentialParentId).Select(c => c.ParentID).FirstOrDefaultAsync(cancellationToken);

			while (currentParentId.HasValue)
			{
				if (currentParentId.Value == categoryId)
				{
					return true;
				}

				currentParentId = await DbSet.Where(c => c.ID == currentParentId.Value).Select(c => c.ParentID).FirstOrDefaultAsync(cancellationToken);
			}

			return false;
		}

		public async Task<bool> HasChilderAsync(int categoryId, CancellationToken cancellationToken = default)
		{
			return await DbSet.AnyAsync(c => c.ParentID == categoryId && !c.IsDeleted, cancellationToken);
		}

		public async Task<IReadOnlyList<int>> GetDescendantIdsAsync(int categoryId, CancellationToken cancellationToken)
		{
			var categories = await DbSet.AsNoTracking().Where(c => !c.IsDeleted).Select(c => new
			{
				c.ID,
				c.ParentID
			}).ToListAsync(cancellationToken);

			var childrenLookUp = categories.Where(c => c.ParentID.HasValue).GroupBy(c => c.ParentID!.Value).ToDictionary(group => group.Key, group => group.Select(c => c.ID).ToList());

			var result = new List<int>();

			var queue = new Queue<int>();

			queue.Enqueue(categoryId);

			while (queue.Count > 0)
			{
				var currentId = queue.Dequeue();

				if (!childrenLookUp.TryGetValue(currentId, out var children))
				{
					continue;
				}

				foreach (var childId in children)
				{
					result.Add(childId);

					queue.Enqueue(childId);
				}
			}

			return result;
		}
	}
}
