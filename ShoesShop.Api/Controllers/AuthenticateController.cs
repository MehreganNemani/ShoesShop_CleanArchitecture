using Microsoft.AspNetCore.Mvc;
using ShoesShop.Application.Contracts.Identity;
using ShoesShop.Application.Identity;

namespace ShoesShop.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthenticateController : ControllerBase
	{
		private readonly IAuthService _authService;

		public AuthenticateController(IAuthService authService)
		{
			_authService = authService;
		}

		[HttpPost("Login")]
		public async Task<ActionResult<AuthResponse>> Login(AuthRequest request)
		{
			return Ok(await _authService.Login(request));
		}

		[HttpPost("Register")]
		public async Task<ActionResult<RegisterationResponse>> Register(RegisterationRequest request)
		{
			return Ok(await _authService.Register(request));
		}
	}
}
