using ShoesShop.MVC.Models.Galleries;
using ShoesShop.MVC.Services.Interfaces;
using System.Net.Http.Headers;

namespace ShoesShop.MVC.Services
{
	public sealed class GalleryApiClient : IGalleryApiClient
	{
		private readonly HttpClient _httpClient;

		public GalleryApiClient(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		public async Task<List<GalleryImageVM>> GetByProductIdAsync(int productId, CancellationToken cancellationToken = default)
		{
			if (productId <= 0)
			{
				throw new ArgumentException("ID Is Not Valid...", nameof(productId));
			}

			using var response = await _httpClient.GetAsync($"api/Admin/Galleries/product/{productId}", cancellationToken);

			response.EnsureSuccessStatusCode();

			var images = await response.Content.ReadFromJsonAsync<List<GalleryImageVM>>(cancellationToken: cancellationToken);

			return images ?? new List<GalleryImageVM>();
		}

		public async Task<bool> AddImageAsync(int productId, IEnumerable<IFormFile> images, CancellationToken cancellationToken = default)
		{
			using var content = new MultipartFormDataContent();

			foreach (var image in images)
			{
				var stream = image.OpenReadStream();
				var streamContent = new StreamContent(stream);

				streamContent.Headers.ContentType = new MediaTypeHeaderValue(
					string.IsNullOrWhiteSpace(image.ContentType) ? "application/octet-stream" : image.ContentType
					);

				content.Add(streamContent, "images", image.FileName);
			}

			using var response = await _httpClient.PostAsync($"api/Admin/Galleries/product/{productId}", content, cancellationToken);

			return response.IsSuccessStatusCode;
		}

		public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
		{
			using var response = await _httpClient.DeleteAsync($"api/Admin/Galleries/{id}", cancellationToken);

			return response.IsSuccessStatusCode;
		}

		public async Task<bool> SetMainAsync(int id, CancellationToken cancellationToken = default)
		{
			using var response = await _httpClient.PutAsync($"api/Admin/Galleries/{id}/main", content: null, cancellationToken);

			return response.IsSuccessStatusCode;
		}
	}
}
