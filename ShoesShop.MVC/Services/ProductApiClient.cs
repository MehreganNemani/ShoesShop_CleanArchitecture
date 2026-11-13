using ShoesShop.MVC.Models;
using ShoesShop.MVC.Models.Products;
using ShoesShop.MVC.Services.Interfaces;

namespace ShoesShop.MVC.Services
{
	public class ProductApiClient : BaseApiClient, IProductApiClient
	{
		private readonly HttpClient _httpClient;

		public ProductApiClient(HttpClient httpClient) : base(httpClient)
		{
			_httpClient = httpClient;
		}

		public async Task<PaginatedViewModel<ProductVM>> GetPagedAsync(string? search, int pageNumber, int pageSize, CancellationToken cancellationToken = default)
		{
			var url = $"api/Admin/Products?pageNumber={pageNumber}" + $"&pageSize={pageSize}";

			if (!string.IsNullOrWhiteSpace(search))
			{
				url += $"&search={Uri.EscapeDataString(search)}";
			}

			var result = await GetAsync<PaginatedViewModel<ProductVM>>(url, cancellationToken);

			return result ?? new PaginatedViewModel<ProductVM>();
		}
		public Task<ProductVM?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
		{
			return GetAsync<ProductVM>($"api/Admin/Products/{id}", cancellationToken);
		}

		public Task<ApiResult> CreateAsync(CreateProductVM model, CancellationToken cancellationToken = default)
		{
			return PostAsync("api/Admin/Products", model, cancellationToken);
		}

		public Task<ApiResult> UpdateAsync(int id, EditProductVM model, CancellationToken cancellationToken = default)
		{
			return PutAsync($"api/Admin/Products/{id}", model, cancellationToken);
		}

		public Task<ApiResult> DeleteAsync(int id, CancellationToken cancellationToken = default)
		{
			return DeleteAsync($"api/Admin/Products/{id}", cancellationToken);
		}

		public async Task<ProductInfoDto?> GetByIdAsync(int productId)
		{
			return await _httpClient.GetFromJsonAsync<ProductInfoDto>($"api/Admin/Products/{productId}");
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

	}
}
