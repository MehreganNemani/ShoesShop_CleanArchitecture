using ShoesShop.MVC.Models.Color;
using ShoesShop.MVC.Models.ProductSizes;
using ShoesShop.MVC.Models.Sizes;
using ShoesShop.MVC.Services.Interfaces;
using System.Text.Json;

namespace ShoesShop.MVC.Services
{
	public class ProductSizeApiClient : IProductSizeApiClient
	{
		private readonly HttpClient _httpClient;
		public ProductSizeApiClient(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		public async Task<List<ProductSizeDto>> GetByProductIdAsync(int productId)
		{
			var result = await _httpClient.GetFromJsonAsync<List<ProductSizeDto>>($"api/Admin/ProductSizes/{productId}");

			return result ?? new List<ProductSizeDto>();
		}
		public async Task<List<SizeVM>> GetAllAsync()
		{
			var result = await _httpClient.GetFromJsonAsync<List<SizeVM>>("api/Admin/ProductSizes/GetAllSizes");

			return result ?? new List<SizeVM>();
		}

		public async Task<(bool Success, string Message)> AddAsync(int productId, int sizeId)
		{
			var response = await _httpClient.PostAsJsonAsync("api/Admin/ProductSizes", new
			{
				ProductID = productId,
				SizeID = sizeId
			});

			var content = await response.Content.ReadAsStringAsync();

			if (response.IsSuccessStatusCode)
			{
				return (true, "Add Size To Product Successfully");
			}

			try
			{
				using var document = JsonDocument.Parse(content);

				if (document.RootElement.TryGetProperty("message", out var message))
				{
					return (false, message.GetString() ?? "Operation Was Failed...");
				}
			}
			catch (JsonException)
			{

			}

			return (false, "Add Size To Product Failed...");
		}



		public async Task<bool> RemoveAsync(int productId, int sizeId)
		{
			var response = await _httpClient.DeleteAsync($"api/Admin/ProductSizes/{productId}/{sizeId}");

			return response.IsSuccessStatusCode;
		}
	}
}
