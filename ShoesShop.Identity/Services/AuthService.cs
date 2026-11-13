using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ShoesShop.Application.Constance;
using ShoesShop.Application.Contracts.Identity;
using ShoesShop.Application.Exceptions;
using ShoesShop.Application.Identity;
using ShoesShop.Identity.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ShoesShop.Identity.Services
{
	public class AuthService : IAuthService
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly IOptions<JWTSettings> _jwtSettings;
		private readonly SignInManager<ApplicationUser> _signInManager;

		public AuthService(UserManager<ApplicationUser> userManager,
						   IOptions<JWTSettings> jwtSettings,
						   SignInManager<ApplicationUser> signInManager)
		{
			_userManager = userManager;
			_jwtSettings = jwtSettings;
			_signInManager = signInManager;
		}

		public async Task<AuthResponse> Login(AuthRequest request)
		{
			var user = await _userManager.FindByEmailAsync(request.Email);

			if (user == null)
			{
				throw new BadRequestException($"User With Email {request.Email} Not Found...");
			}

			var result = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, false, lockoutOnFailure: false);

			if (!result.Succeeded)
			{
				throw new BadRequestException($"Credentials For {request.Email} Are Not Valid...");
			}

			JwtSecurityToken jwtSecurityToken = await GenerateToken(user);

			AuthResponse response = new AuthResponse()
			{
				Id = user.Id,
				Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
				Email = user.Email,
				UserName = user.UserName,
			};

			return response;
		}

		#region Register
		public async Task<RegisterationResponse> Register(RegisterationRequest request)
		{
			var existingUser = await _userManager.FindByNameAsync(request.UserName);

			if (existingUser != null)
			{
				throw new BadRequestException($"User Name {request.UserName} Already Exists...");
			}

			var user = new ApplicationUser
			{
				Email = request.Email,
				FirstName = request.FirstName,
				LastName = request.LastName,
				UserName = request.UserName,
				EmailConfirmed = true
			};

			var existingEmail = await _userManager.FindByEmailAsync(request.Email);

			if (existingEmail == null)
			{
				var result = await _userManager.CreateAsync(user, request.Password);

				if (result.Succeeded)
				{
					await _userManager.AddToRoleAsync(user, "User");
					return new RegisterationResponse() { UserId = user.Id };
				}
				else
				{
					throw new BadRequestException(string.Join(" | ", result.Errors.Select(x => x.Description)));
				}
			}
			else
			{
				throw new BadRequestException($"Email {request.Email} Already Exists...");
			}
		}

		#endregion

		private async Task<JwtSecurityToken> GenerateToken(ApplicationUser user)
		{
			var userClaims = await _userManager.GetClaimsAsync(user);
			var roles = await _userManager.GetRolesAsync(user);
			var roleClaims = new List<Claim>();
			for (int i = 0; i < roles.Count; i++)
			{
				roleClaims.Add(new Claim(ClaimTypes.Role, roles[i]));
			}

			var claims = new[]
			{
				new Claim(JwtRegisteredClaimNames.Sub,user.UserName),
				new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
				new Claim(JwtRegisteredClaimNames.Email,user.Email),
				new Claim(CustomClaimTypes.Uid,user.Id)
			}.Union(userClaims)
			.Union(roleClaims);

			var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Value.Key));

			var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

			var jwtSecurityToken = new JwtSecurityToken(
				issuer: _jwtSettings.Value.Issuer,
				audience: _jwtSettings.Value.Audience,
				claims: claims,
				expires: DateTime.UtcNow.AddMinutes(_jwtSettings.Value.DurationInMinutes),
				signingCredentials: signingCredentials
				);

			return jwtSecurityToken;
		}
	}
}
