using ShoesShop.MVC.Models.Account;
using ShoesShop.MVC.Services.Interfaces;

namespace ShoesShop.MVC.Services
{
	public class AccountApiClient : IAccountApiClient
	{
		private readonly HttpClient _httpClient;

		public AccountApiClient(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}
		public async Task<HttpResponseMessage> RegisterAsync(RegistrationRequestDto request)
		{
			return await _httpClient.PostAsJsonAsync("api/Authenticate/Register", request);
		}

		public async Task<HttpResponseMessage> LoginAsync(AuthRequestDto request)
		{
			return await _httpClient.PostAsJsonAsync("api/Authenticate/Login", request);
		}
	}
}
