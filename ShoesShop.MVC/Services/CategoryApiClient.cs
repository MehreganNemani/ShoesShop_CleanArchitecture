using ShoesShop.Api.Contracts.Categories;
using ShoesShop.MVC.Models;
using ShoesShop.MVC.Models.Categories;
using ShoesShop.MVC.Services.Interfaces;

namespace ShoesShop.MVC.Services
{
	public sealed class CategoryApiClient : BaseApiClient, ICategoryApiClient
	{
		public CategoryApiClient(HttpClient httpClient) : base(httpClient)
		{

		}

		public async Task<List<CategoryVM>> GetAllAsync(string? search, CancellationToken cancellationToken = default)
		{
			var url = $"api/Admin/Categories";
			if (!string.IsNullOrWhiteSpace(search))
			{
				url += $"?&search={Uri.EscapeDataString(search)}";
			}

			var result = await GetAsync<List<CategoryVM>>(url, cancellationToken);

			return result ?? new List<CategoryVM>();
		}

		public Task<CategoryVM?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
		{
			return GetAsync<CategoryVM>($"api/Admin/Categories/{id}", cancellationToken);
		}

		public Task<ApiResult> CreateAsync(CreateCategoryVM model, CancellationToken cancellationToken = default)
		{
			return PostAsync("api/Admin/Categories", model, cancellationToken);
		}

		public Task<ApiResult> UpdateAsync(int id, UpdateCategoryVM model, CancellationToken cancellationToken = default)
		{
			return PutAsync($"api/Admin/Categories/{id}", model, cancellationToken);
		}

		public Task<ApiResult> DeleteAsync(int id, CancellationToken cancellationToken = default)
		{
			return DeleteAsync($"api/Admin/Categories/{id}", cancellationToken);
		}
		public async Task<IReadOnlyList<CategoryOptionDto>> GetOptionsAsync(CancellationToken cancellationToken = default)
		{
			var result = await GetAsync<List<CategoryOptionDto>>("api/Admin/Categories/options", cancellationToken);
			return result ?? new List<CategoryOptionDto>();
		}

		public async Task<IReadOnlyList<CategoryOptionDto>> GetAvailableParentsAsync(int categoryId, CancellationToken cancellationToken)
		{
			var result = await GetAsync<List<CategoryOptionDto>>($"api/Admin/Categories/{categoryId}/available-parents", cancellationToken);
			return result ?? new List<CategoryOptionDto>();
		}
	}
}
