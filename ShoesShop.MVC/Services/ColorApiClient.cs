using ShoesShop.MVC.Models;
using ShoesShop.MVC.Models.Color;
using ShoesShop.MVC.Services.Interfaces;

namespace ShoesShop.MVC.Services
{
	public sealed class ColorApiClient : BaseApiClient, IColorApiClient
	{
		private readonly HttpClient _httpClient;
		public ColorApiClient(HttpClient httpClient) : base(httpClient)
		{
			_httpClient = httpClient;
		}

		public async Task<PaginatedViewModel<ColorVM>> GetPagedAsync(string? search, int pageNumber, int pageSize, CancellationToken cancellationToken = default)
		{
			var url = $"api/Admin/Colors?pageNumber={pageNumber}" + $"&pageSize={pageSize}";

			if (!string.IsNullOrWhiteSpace(search))
			{
				url += $"&search={Uri.EscapeDataString(search)}";
			}

			var result = await GetAsync<PaginatedViewModel<ColorVM>>(url, cancellationToken);

			return result ?? new PaginatedViewModel<ColorVM>();
		}

		public Task<ColorVM?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
		{
			return GetAsync<ColorVM>($"api/Admin/Colors/{id}", cancellationToken);
		}

		public Task<ApiResult> CreateAsync(CreateColorVM model, CancellationToken cancellationToken = default)
		{
			return PostAsync("api/Admin/Colors", model, cancellationToken);
		}

		public Task<ApiResult> UpdateAsync(int id, EditColorVM model, CancellationToken cancellationToken = default)
		{
			return PutAsync($"api/Admin/Colors/{id}", model, cancellationToken);
		}

		public Task<ApiResult> DeleteAsync(int id, CancellationToken cancellationToken = default)
		{
			return DeleteAsync($"api/Admin/Colors/{id}", cancellationToken);
		}

		private static async Task<string> GetErrorMessageAsync(HttpResponseMessage response, CancellationToken cancellationToken)
		{
			try
			{
				var error = await response.Content.ReadFromJsonAsync<ApiValidationErrorResponse>(cancellationToken: cancellationToken);
				if (error?.Errors is null)
				{
					return "An Error Has Occured...";
				}

				var messages = error.Errors.SelectMany(e => e.Value).Where(e => !string.IsNullOrWhiteSpace(e));

				var message = string.Join(Environment.NewLine, messages);

				return string.IsNullOrWhiteSpace(message) ? "An Error Has Occured..." : message;
			}
			catch
			{
				return "An Error Has Occured...";
			}
		}

		public async Task<List<ColorVM>> GetAllAsync()
		{
			var result = await _httpClient.GetFromJsonAsync<List<ColorVM>>("api/Admin/Colors/GetAllColors");

			return result ?? new List<ColorVM>();
		}
	}
}
