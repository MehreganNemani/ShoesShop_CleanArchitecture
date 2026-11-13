using ShoesShop.MVC.Models.ProductColors;

namespace ShoesShop.MVC.Services.Interfaces
{
	public interface IProductColorApiClient
	{
		Task<List<ProductColorDto>> GetByProductIdAsync(int productId);

		Task<(bool Success, string Message)> AddAsync(int productId, int colorId);

		Task<bool> RemoveAsync(int productId, int colorId);
	}
}
