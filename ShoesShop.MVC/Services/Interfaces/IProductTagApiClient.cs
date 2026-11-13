using ShoesShop.MVC.Models.ProductTags;

namespace ShoesShop.MVC.Services.Interfaces
{
	public interface IProductTagApiClient
	{
		Task<List<ProductTagDto>> GetByProductIdAsync(int productId);

		Task<(bool Success, string Message)> AddAsync(int productId, int tagId);

		Task<bool> RemoveAsync(int productId, int tagId);
	}
}
