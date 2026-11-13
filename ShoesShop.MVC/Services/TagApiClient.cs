using ShoesShop.MVC.Models;
using ShoesShop.MVC.Models.Tags;
using ShoesShop.MVC.Services.Interfaces;

namespace ShoesShop.MVC.Services
{
	public sealed class TagApiClient : BaseApiClient, ITagApiClient
	{
		private readonly HttpClient _httpClient;
		public TagApiClient(HttpClient httpClient) : base(httpClient)
		{
			_httpClient = httpClient;
		}
		public async Task<PaginatedViewModel<TagVM>> GetPagedAsync(string? search, int pageNumber, int pageSize, CancellationToken cancellationToken = default)
		{
			var url = $"api/Admin/Tags?pageNumber={pageNumber}" + $"&pageSize={pageSize}";

			if (!string.IsNullOrWhiteSpace(search))
			{
				url += $"&search={Uri.EscapeDataString(search)}";
			}

			var result = await GetAsync<PaginatedViewModel<TagVM>>(url, cancellationToken);

			return result ?? new PaginatedViewModel<TagVM>();
		}

		public Task<TagVM?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
		{
			return GetAsync<TagVM>($"api/Admin/Tags/{id}", cancellationToken);
		}

		public Task<ApiResult> CreateAsync(CreateTagVM model, CancellationToken cancellationToken = default)
		{
			return PostAsync("api/Admin/Tags", model, cancellationToken);
		}
		public Task<ApiResult> UpdateAsync(int id, EditTagVM model, CancellationToken cancellationToken = default)
		{
			return PutAsync($"api/Admin/Tags/{id}", model, cancellationToken);
		}
		public Task<ApiResult> DeleteAsync(int id, CancellationToken cancellationToken = default)
		{
			return DeleteAsync($"api/Admin/Tags/{id}", cancellationToken);
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

		public async Task<List<TagVM>> GetAllAsync()
		{
			var result = await _httpClient.GetFromJsonAsync<List<TagVM>>("api/Admin/Tags/GetAllTags");

			return result ?? new List<TagVM>();
		}
	}
}
