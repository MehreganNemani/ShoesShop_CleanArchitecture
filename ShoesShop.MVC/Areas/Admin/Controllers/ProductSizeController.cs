using Microsoft.AspNetCore.Mvc;
using ShoesShop.MVC.Models.ProductSizes;
using ShoesShop.MVC.Services.Interfaces;

namespace ShoesShop.MVC.Areas.Admin.Controllers
{
	[Area("Admin")]
	[AutoValidateAntiforgeryToken]
	[RequireHttps]
	public class ProductSizeController : Controller
	{
		private readonly IProductSizeApiClient _productSizeApiClient;
		private readonly IProductApiClient _productApiClient;

		public ProductSizeController(IProductSizeApiClient productSizeApiClient, IProductApiClient productApiClient)
		{
			_productSizeApiClient = productSizeApiClient;
			_productApiClient = productApiClient;
		}

		public async Task<IActionResult> Index(int productId)
		{
			var productInfo = await _productApiClient.GetByIdAsync(productId);

			if (productInfo == null)
			{
				return NotFound("Product Does Not Exists...");
			}

			var productSizes = await _productSizeApiClient.GetByProductIdAsync(productId);

			var allSizes = await _productSizeApiClient.GetAllAsync();

			var selectedSizeIds = productSizes.Select(s => s.SizeID).ToHashSet();

			var availableSizes = allSizes.Where(s => !selectedSizeIds.Contains(s.Id)).ToList();

			var model = new ProductSizeIndexVM
			{
				ProductID = productId,
				ProductInfo = productInfo,
				ProductSizes = productSizes ?? new List<ProductSizeDto>(),
				AvailableSizes = availableSizes
			};

			return View(model);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Add(int productId, int sizeId)
		{
			if (productId <= 0 || sizeId <= 0)
			{
				TempData["Error"] = "Product Or Size Is Not Valid...";
				return RedirectToAction(nameof(Index), new { productId });
			}

			var result = await _productSizeApiClient.AddAsync(productId, sizeId);

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
		public async Task<IActionResult> Remove(int productId, int sizeId)
		{
			if (productId <= 0 || sizeId <= 0)
			{
				TempData["Error"] = "Product Or Size Is Not Valid...";
				return RedirectToAction(nameof(Index), new { productId });
			}

			var success = await _productSizeApiClient.RemoveAsync(productId, sizeId);

			if (success)
			{
				TempData["Success"] = "Remove Size From Product Successfully";
			}
			else
			{
				TempData["Error"] = "Remove Size From Product Failed...";
			}

			return RedirectToAction(nameof(Index), new { productId });
		}
	}
}
