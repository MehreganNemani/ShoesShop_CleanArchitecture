using ShoesShop.MVC.Models.ProductTags;
using ShoesShop.MVC.Services.Interfaces;
using System.Text.Json;

namespace ShoesShop.MVC.Services
{
	public class ProductTagApiClient : IProductTagApiClient
	{
		private readonly HttpClient _httpClient;

		public ProductTagApiClient(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}
		public async Task<List<ProductTagDto>> GetByProductIdAsync(int productId)
		{
			var result = await _httpClient.GetFromJsonAsync<List<ProductTagDto>>($"api/Admin/ProductTags/{productId}");

			return result ?? new List<ProductTagDto>();
		}

		public async Task<(bool Success, string Message)> AddAsync(int productId, int tagId)
		{
			var response = await _httpClient.PostAsJsonAsync("api/Admin/ProductTags", new
			{
				ProductId = productId,
				TagId = tagId
			});

			var content = await response.Content.ReadAsStringAsync();

			if (response.IsSuccessStatusCode)
			{
				return (true, "Add Tag To Product Successfully");
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

			return (false, "Add Tag To Product Failed...");
		}

		public async Task<bool> RemoveAsync(int productId, int tagId)
		{
			var response = await _httpClient.DeleteAsync($"api/Admin/ProductTags/{productId}/{tagId}");

			return response.IsSuccessStatusCode;
		}
	}
}
