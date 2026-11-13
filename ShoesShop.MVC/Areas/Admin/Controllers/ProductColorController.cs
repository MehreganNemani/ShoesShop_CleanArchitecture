using Microsoft.AspNetCore.Mvc;
using ShoesShop.MVC.Models.ProductColors;
using ShoesShop.MVC.Services.Interfaces;

namespace ShoesShop.MVC.Areas.Admin.Controllers
{
	[Area("Admin")]
	[AutoValidateAntiforgeryToken]
	[RequireHttps]
	public class ProductColorController : Controller
	{
		private readonly IProductColorApiClient _productColorApiClient;

		private readonly IColorApiClient _colorApiClient;

		private readonly IProductApiClient _productApiClient;
		public ProductColorController(IProductColorApiClient productColorApiClient, IColorApiClient colorApiClient, IProductApiClient productApiClient)
		{
			_productColorApiClient = productColorApiClient;
			_colorApiClient = colorApiClient;
			_productApiClient = productApiClient;
		}

		public async Task<IActionResult> Index(int productId)
		{
			var productInfo = await _productApiClient.GetByIdAsync(productId);

			if (productInfo == null)
			{
				return NotFound("Product Does Not Exists...");
			}

			var productColors = await _productColorApiClient.GetByProductIdAsync(productId);

			var allColors = await _colorApiClient.GetAllAsync();

			var selectedColorIds = productColors.Select(c => c.ColorID).ToHashSet();

			var availableColors = allColors.Where(c => !selectedColorIds.Contains(c.Id)).ToList();

			var model = new ProductColorIndexVM
			{
				ProductID = productId,
				ProductInfo = productInfo,
				ProductColors = productColors ?? new List<ProductColorDto>(),
				AvailableColors = availableColors
			};

			return View(model);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Add(int productId, int colorId)
		{
			if (productId <= 0 || colorId <= 0)
			{
				TempData["Error"] = "Product Or Color Is Not Valid...";
				return RedirectToAction(nameof(Index), new { productId });
			}

			var result = await _productColorApiClient.AddAsync(productId, colorId);

			if (result.Success)
			{
				TempData["Success"] = result.Message;
			}
			else
			{
				TempData["Error"] = result.Message;
			}

			return RedirectToAction(nameof(Index), new { productId });
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Remove(int productId, int colorId)
		{
			if (productId <= 0 || colorId <= 0)
			{
				TempData["Error"] = "Product Or Color Is Not Valid...";
				return RedirectToAction(nameof(Index), new { productId });
			}

			var success = await _productColorApiClient.RemoveAsync(productId, colorId);

			if (success)
			{
				TempData["Success"] = "Remove Color From Product Successfully";
			}
			else
			{
				TempData["Error"] = "Remove Color From Product Failed...";
			}

			return RedirectToAction(nameof(Index), new { productId });
		}



	}
}
