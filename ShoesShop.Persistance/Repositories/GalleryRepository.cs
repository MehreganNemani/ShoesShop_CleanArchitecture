using Microsoft.EntityFrameworkCore;
using ShoesShop.Application.Common.Interfaces;
using ShoesShop.Domain.Entities.Products;

namespace ShoesShop.Persistance.Repositories
{
	public class GalleryRepository : IGalleryRepository
	{
		private readonly ShoesShop_DbContext _context;

		public GalleryRepository(ShoesShop_DbContext context)
		{
			_context = context;
		}

		public Task<Gallery?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
		{
			return _context.Set<Gallery>().FirstOrDefaultAsync(g => g.ID == id, cancellationToken);
		}

		public Task<List<Gallery>> GetByProductIdAsync(int productId, CancellationToken cancellationToken = default)
		{
			return _context.Set<Gallery>().AsNoTracking().Where(g => g.ProductID == productId).OrderByDescending(g => g.IsMain).ThenBy(g => g.ID).ToListAsync(cancellationToken);
		}

		public async Task AddAsync(Gallery gallery, CancellationToken cancellationToken = default)
		{
			await _context.Set<Gallery>().AddAsync(gallery, cancellationToken);
		}

		public void Remove(Gallery gallery)
		{
			_context.Set<Gallery>().Remove(gallery);
		}

		public Task<bool> ProductExistsAsync(int productId, CancellationToken cancellationToken = default)
		{
			return _context.Set<Product>().AnyAsync(g => g.ID == productId && !g.IsDeleted, cancellationToken);
		}

		public Task<List<Gallery>> GetTrackByProductIdAsync(int productId, CancellationToken cancellationToken = default)
		{
			return _context.Set<Gallery>().Where(g => g.ProductID == productId).OrderByDescending(g => g.IsMain).ThenBy(g => g.ID).ToListAsync(cancellationToken);
		}

		public async Task ExecuteInTransactionAsync(Func<CancellationToken, Task> action, CancellationToken cancellationToken = default)
		{
			await using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);

			try
			{
				await action(cancellationToken);

				await transaction.CommitAsync(cancellationToken);
			}
			catch
			{
				await transaction.RollbackAsync(cancellationToken);
				throw;
			}
		}
	}
}
