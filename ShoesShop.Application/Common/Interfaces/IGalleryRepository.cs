using ShoesShop.Domain.Entities.Products;

namespace ShoesShop.Application.Common.Interfaces
{
	public interface IGalleryRepository
	{
		Task<Gallery?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
		Task<List<Gallery>> GetByProductIdAsync(int productId, CancellationToken cancellationToken = default);

		Task AddAsync(Gallery gallery, CancellationToken cancellationToken = default);
		void Remove(Gallery gallery);

		Task<bool> ProductExistsAsync(int productId, CancellationToken cancellationToken = default);

		Task<List<Gallery>> GetTrackByProductIdAsync(int productId, CancellationToken cancellationToken = default);

		Task ExecuteInTransactionAsync(Func<CancellationToken, Task> action, CancellationToken cancellationToken = default);
	}
}
