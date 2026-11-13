using ShoesShop.MVC.Models;
using System.Net;

namespace ShoesShop.MVC.Services
{
	public abstract class BaseApiClient
	{
		protected readonly HttpClient _httpclient;

		protected BaseApiClient(HttpClient httpClient)
		{
			_httpclient = httpClient;
		}

		protected async Task<ApiResult> HandleResponseAsync(HttpResponseMessage response, CancellationToken cancellationToken = default)
		{
			if (response.IsSuccessStatusCode)
			{
				return ApiResult.Ok();
			}

			var message = await GetErrorMessageAsync(response, cancellationToken);

			return ApiResult.Fail(message);
		}

		protected async Task<T?> GetAsync<T>(string url, CancellationToken cancellationToken = default)
		{
			var response = await _httpclient.GetAsync(url, cancellationToken);

			if (response.StatusCode == HttpStatusCode.NotFound)
			{
				return default;
			}

			response.EnsureSuccessStatusCode();

			return await response.Content.ReadFromJsonAsync<T>(cancellationToken: cancellationToken);
		}

		protected async Task<ApiResult> PostAsync<T>(string url, T data, CancellationToken cancellationToken = default)
		{
			var response = await _httpclient.PostAsJsonAsync(url, data, cancellationToken: cancellationToken);

			return await HandleResponseAsync(response, cancellationToken);
		}

		protected async Task<ApiResult> PutAsync<T>(string url, T data, CancellationToken cancellationToken = default)
		{
			var response = await _httpclient.PutAsJsonAsync(url, data, cancellationToken: cancellationToken);

			return await HandleResponseAsync(response, cancellationToken);
		}

		protected async Task<ApiResult> DeleteAsync(string url, CancellationToken cancellationToken = default)
		{
			var response = await _httpclient.DeleteAsync(url, cancellationToken: cancellationToken);

			return await HandleResponseAsync(response, cancellationToken);
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
