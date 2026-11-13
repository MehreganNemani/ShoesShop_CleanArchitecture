using ShoesShop.MVC.Models.ProductSizes;
using ShoesShop.MVC.Models.Sizes;

namespace ShoesShop.MVC.Services.Interfaces
{
	public interface IProductSizeApiClient
	{
		Task<List<ProductSizeDto>> GetByProductIdAsync(int productId);
		Task<(bool Success, string Message)> AddAsync(int productId, int sizeId);
		Task<bool> RemoveAsync(int productId, int sizeId);
		Task<List<SizeVM>> GetAllAsync();
	}
}
