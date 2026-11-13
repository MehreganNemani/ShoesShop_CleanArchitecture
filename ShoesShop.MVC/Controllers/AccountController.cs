using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using ShoesShop.MVC.Models.Account;
using ShoesShop.MVC.Services.Interfaces;
using System.Security.Claims;

namespace ShoesShop.MVC.Controllers
{
	[AutoValidateAntiforgeryToken]
	[RequireHttps]
	public class AccountController : Controller
	{
		private readonly IAccountApiClient _accountApiClient;

		public AccountController(IAccountApiClient accountApiClient)
		{
			_accountApiClient = accountApiClient;
		}

		[HttpGet]
		public IActionResult Register()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Register(RegisterVM model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			var request = new RegistrationRequestDto
			{
				FirstName = model.FirstName,
				LastName = model.LastName,
				UserName = model.UserName,
				Email = model.Email,
				Password = model.Password
			};

			using var response = await _accountApiClient.RegisterAsync(request);

			if (response.IsSuccessStatusCode)
			{
				TempData["SuccessMessage"] = "Registration Completed Successfully...";

				return RedirectToAction(nameof(Login));
			}

			var apiError = await response.Content.ReadFromJsonAsync<ApiErrorResponseDto>();

			if (apiError?.Errors != null)
			{
				foreach (var error in apiError.Errors)
				{
					foreach (var message in error.Value)
					{
						ModelState.AddModelError(error.Key, message);
					}
				}
			}

			if (!string.IsNullOrWhiteSpace(apiError?.Message))
			{
				ModelState.AddModelError(string.Empty, apiError.Message);
			}

			if (ModelState.ErrorCount == 0)
			{
				ModelState.AddModelError(string.Empty, "Registration Failed, Please Try Again...");
			}

			return View(model);
		}

		[HttpGet]
		public IActionResult Login()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Login(LoginVM model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			var request = new AuthRequestDto
			{
				Email = model.Email,
				Password = model.Password
			};

			using var response = await _accountApiClient.LoginAsync(request);

			if (!response.IsSuccessStatusCode)
			{
				var apiError = await response.Content.ReadFromJsonAsync<ApiErrorResponseDto>();

				if (!string.IsNullOrWhiteSpace(apiError?.Message))
				{
					ModelState.AddModelError("", apiError.Message);
				}
				else
				{
					ModelState.AddModelError("", "Invalid Email Or Password...");
				}

				return View(model);
			}

			var authResponse = await response.Content.ReadFromJsonAsync<AuthResponseDto>();

			if (authResponse == null || string.IsNullOrWhiteSpace(authResponse.Token))
			{
				ModelState.AddModelError("", "Authentication Response Is Invalid...");
				return View(model);
			}

			var claims = new List<Claim>
			{
			   new Claim(ClaimTypes.NameIdentifier , authResponse.Id),
			   new Claim(ClaimTypes.Name , authResponse.UserName),
			   new Claim(ClaimTypes.Email , authResponse.Email),
			   new Claim("AccessToken",authResponse.Token)
			};

			var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
			var principal = new ClaimsPrincipal(identity);

			await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties
			{
				IsPersistent = model.RememberMe
			});

			return RedirectToAction("Index", "Home");
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Logout()
		{
			await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

			return RedirectToAction("Index", "Home");
		}
	}
}
