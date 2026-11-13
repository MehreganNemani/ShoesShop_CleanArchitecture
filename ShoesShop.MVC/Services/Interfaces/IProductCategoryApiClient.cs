using ShoesShop.MVC.Models.Categories;
using ShoesShop.MVC.Models.ProductCategories;

namespace ShoesShop.MVC.Services.Interfaces
{
	public interface IProductCategoryApiClient
	{
		Task<List<CategoryDtoForProductCategory>> GetParentCategoriesAsync();
		Task<List<CategoryDtoForProductCategory>> GetChildCategoriesAsync(int parentId);
		Task<List<ProductCategoryDto>> GetProductCategoriesAsync(int productId);
		Task<(bool Success, string Message)> AddProductCategoryAsync(AddProductCategoryVM model);
		Task<bool> RemoveProductCategoryAsync(int productId, int categoryId);
	}
}
