using ShoesShop.MVC.Models.Galleries;

namespace ShoesShop.MVC.Services.Interfaces
{
	public interface IGalleryApiClient
	{
		Task<List<GalleryImageVM>> GetByProductIdAsync(int productId, CancellationToken cancellationToken = default);
		Task<bool> AddImageAsync(int productId, IEnumerable<IFormFile> images, CancellationToken cancellationToken = default);
		Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
		Task<bool> SetMainAsync(int id, CancellationToken cancellationToken = default);
	}
}
