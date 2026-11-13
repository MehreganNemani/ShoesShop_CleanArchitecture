using Microsoft.AspNetCore.Mvc;
using ShoesShop.MVC.Models.ProductTags;
using ShoesShop.MVC.Services.Interfaces;

namespace ShoesShop.MVC.Areas.Admin.Controllers
{
	[Area("Admin")]
	[AutoValidateAntiforgeryToken]
	[RequireHttps]
	public class ProductTagController : Controller
	{
		private readonly IProductTagApiClient _productTagApiClient;

		private readonly ITagApiClient _tagApiClient;

		private readonly IProductApiClient _productApiClient;

		public ProductTagController(IProductTagApiClient productTagApiClient, ITagApiClient tagApiClient, IProductApiClient productApiClient)
		{
			_productTagApiClient = productTagApiClient;
			_tagApiClient = tagApiClient;
			_productApiClient = productApiClient;
		}

		public async Task<IActionResult> Index(int productId)
		{
			var productInfo = await _productApiClient.GetByIdAsync(productId);

			if (productInfo == null)
			{
				return NotFound("Product Does Not Exists...");
			}

			var productTags = await _productTagApiClient.GetByProductIdAsync(productId);

			var allTags = await _tagApiClient.GetAllAsync();

			var selectedTagIds = productTags.Select(t => t.TagID).ToHashSet();

			var availableTags = allTags.Where(t => !selectedTagIds.Contains(t.Id)).ToList();

			var model = new ProductTagIndexVM
			{
				ProductID = productId,
				ProductInfo = productInfo,
				ProductTags = productTags ?? new List<ProductTagDto>(),
				AvailableTags = availableTags
			};

			return View(model);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Add(int productId, int tagId)
		{
			if (productId <= 0 || tagId <= 0)
			{
				TempData["Error"] = "Product Or Tag Is Not Valid...";
				return RedirectToAction(nameof(Index), new { productId });
			}

			var result = await _productTagApiClient.AddAsync(productId, tagId);

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
		public async Task<IActionResult> Remove(int productId, int tagId)
		{
			if (productId <= 0 || tagId <= 0)
			{
				TempData["Error"] = "Product Or Tag Is Not Valid...";
				return RedirectToAction(nameof(Index), new { productId });
			}

			var success = await _productTagApiClient.RemoveAsync(productId, tagId);

			if (success)
			{
				TempData["Success"] = "Remove Tag From Product Successfully";
			}
			else
			{
				TempData["Error"] = "Remove Tag From Product Failed...";
			}

			return RedirectToAction(nameof(Index), new { productId });
		}
	}
}
