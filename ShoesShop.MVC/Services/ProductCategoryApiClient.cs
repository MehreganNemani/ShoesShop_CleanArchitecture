using ShoesShop.MVC.Models.Categories;
using ShoesShop.MVC.Models.ProductCategories;
using ShoesShop.MVC.Services.Interfaces;

namespace ShoesShop.MVC.Services
{
	public class ProductCategoryApiClient : IProductCategoryApiClient
	{
		private readonly HttpClient _httpClient;

		public ProductCategoryApiClient(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		public async Task<List<CategoryDtoForProductCategory>> GetParentCategoriesAsync()
		{
			return await _httpClient.GetFromJsonAsync<List<CategoryDtoForProductCategory>>("api/Admin/ProductCategories/parents")
				?? new List<CategoryDtoForProductCategory>();
		}
		public async Task<List<CategoryDtoForProductCategory>> GetChildCategoriesAsync(int parentId)
		{
			return await _httpClient.GetFromJsonAsync<List<CategoryDtoForProductCategory>>($"api/Admin/ProductCategories/children/{parentId}")
				?? new List<CategoryDtoForProductCategory>();
		}

		public async Task<List<ProductCategoryDto>> GetProductCategoriesAsync(int productId)
		{
			return await _httpClient.GetFromJsonAsync<List<ProductCategoryDto>>($"api/Admin/ProductCategories/product/{productId}")
					?? new List<ProductCategoryDto>();
		}

		public async Task<bool> RemoveProductCategoryAsync(int productId, int categoryId)
		{
			var response = await _httpClient.DeleteAsync($"api/Admin/ProductCategories/product/{productId}/category/{categoryId}");

			return response.IsSuccessStatusCode;
		}

		public async Task<(bool Success, string Message)> AddProductCategoryAsync(AddProductCategoryVM model)
		{
			var response = await _httpClient.PostAsJsonAsync("api/Admin/ProductCategories", model);

			if (response.IsSuccessStatusCode)
			{
				return (true, "Add New Record Successfully");
			}

			var message = await response.Content.ReadAsStringAsync();

			return (false, message);
		}
	}
}
