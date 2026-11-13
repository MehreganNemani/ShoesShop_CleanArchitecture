using ShoesShop.MVC.Models.ProductColors;
using ShoesShop.MVC.Services.Interfaces;
using System.Text.Json;

namespace ShoesShop.MVC.Services
{
	public class ProductColorApiClient : IProductColorApiClient
	{
		private readonly HttpClient _httpClient;

		public ProductColorApiClient(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		public async Task<List<ProductColorDto>> GetByProductIdAsync(int productId)
		{
			var result = await _httpClient.GetFromJsonAsync<List<ProductColorDto>>($"api/Admin/ProductColors/{productId}");

			return result ?? new List<ProductColorDto>();
		}

		public async Task<(bool Success , string Message)> AddAsync(int productId, int colorId)
		{
			var response = await _httpClient.PostAsJsonAsync("api/Admin/ProductColors", new
			{
				ProductId = productId,
				ColorId = colorId
			});

			var content = await response.Content.ReadAsStringAsync();

			if(response.IsSuccessStatusCode)
			{
				return (true, "Add Color To Product Successfully");
			}

			try
			{
				using var document = JsonDocument.Parse(content);

				if(document.RootElement.TryGetProperty("message",out var message))
				{
					return (false, message.GetString() ?? "Operation Was Failed...");
				}
			}
			catch (JsonException)
			{
				
			}

			return (false, "Add Color To Product Failed...");
		}

		public async Task<bool> RemoveAsync(int productId, int colorId)
		{
			var response = await _httpClient.DeleteAsync($"api/Admin/ProductColors/{productId}/{colorId}");

			return response.IsSuccessStatusCode;
		}
	}
}
