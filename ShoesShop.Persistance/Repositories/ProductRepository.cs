using Microsoft.EntityFrameworkCore;
using ShoesShop.Application.Common.Interfaces;
using ShoesShop.Application.Common.Models;
using ShoesShop.Domain.Entities.Products;

namespace ShoesShop.Persistance.Repositories
{
	public sealed class ProductRepository : Repository<Product>, IProductRepository
	{
		public ProductRepository(ShoesShop_DbContext context) : base(context)
		{

		}

		public async Task<bool> ExistsByNameAsync(string productName, int? excludeId = null, CancellationToken cancellationToken = default)
		{
			var normalizedName = productName.Trim();

			return await DbSet.AsNoTracking().AnyAsync(p => !p.IsDeleted && p.ProductName == normalizedName && (!excludeId.HasValue || p.ID != excludeId.Value), cancellationToken);
		}

		public async Task<bool> ExistsByCodeAsync(string productCode, int? excludeId = null, CancellationToken cancellationToken = default)
		{
			var normalizedCode = productCode.Trim();

			return await DbSet.AsNoTracking().AnyAsync(p => !p.IsDeleted && p.ProductCode == normalizedCode && (!excludeId.HasValue || p.ID != excludeId.Value), cancellationToken);
		}

		public async Task<bool> ExistsByModelAsync(string productModel, int? excludeId = null, CancellationToken cancellationToken = default)
		{
			var normalizedModel = productModel.Trim();

			return await DbSet.AsNoTracking().AnyAsync(p => !p.IsDeleted && p.ProductModel == normalizedModel && (!excludeId.HasValue || p.ID != excludeId.Value), cancellationToken);
		}

		public async Task<PaginatedResult<Product>> GetPagedAsync(string? search, int pageNumber, int pageSize, CancellationToken cancellationToken = default)
		{
			IQueryable<Product> query = DbSet.AsNoTracking().Where(p => !p.IsDeleted);

			if (!string.IsNullOrWhiteSpace(search))
			{
				search = search.Trim();

				query = query.Where(p => p.ProductName.Contains(search) || p.ProductModel.Contains(search) || p.ProductCode.Contains(search));
			}

			var totalCount = await query.CountAsync(cancellationToken);

			var items = await query.OrderBy(p => p.ProductName).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);

			return new PaginatedResult<Product>
			{
				Items = items,
				PageNumber = pageNumber,
				PageSize = pageSize,
				TotalCount = totalCount
			};
		}
	}
}
