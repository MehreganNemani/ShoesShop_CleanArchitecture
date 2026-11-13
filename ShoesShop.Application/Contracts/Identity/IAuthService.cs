using ShoesShop.Application.Identity;

namespace ShoesShop.Application.Contracts.Identity
{
	public interface IAuthService
	{
		Task<AuthResponse> Login(AuthRequest request);
		Task<RegisterationResponse> Register(RegisterationRequest request);
	}
}
