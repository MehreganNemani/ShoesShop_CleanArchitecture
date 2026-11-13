using ShoesShop.MVC.Models.Account;

namespace ShoesShop.MVC.Services.Interfaces
{
	public interface IAccountApiClient
	{
		Task<HttpResponseMessage> RegisterAsync(RegistrationRequestDto request);
		Task<HttpResponseMessage> LoginAsync(AuthRequestDto request);
	}
}
