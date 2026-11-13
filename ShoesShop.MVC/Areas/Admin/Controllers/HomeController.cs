using Microsoft.AspNetCore.Mvc;
using ShoesShop.MVC.Models.Products;
using ShoesShop.MVC.Services.Interfaces;

namespace ShoesShop.MVC.Areas.Admin.Controllers
{
	[Area("Admin")]
	[AutoValidateAntiforgeryToken]
	[RequireHttps]
	public class HomeController : Controller
	{
		private readonly IProductApiClient _productApiClient;
		public HomeController(IProductApiClient productApiClient)
		{
			_productApiClient = productApiClient;
		}

		public async Task<IActionResult> Index(string? search, int pageNumber, int pageSize, CancellationToken cancellationToken = default)
		{
			var result = await _productApiClient.GetPagedAsync(search, pageNumber, pageSize, cancellationToken);

			ViewBag.Serach = search;

			return View(result);
		}

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(CreateProductVM model, CancellationToken cancellationToken)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			var result = await _productApiClient.CreateAsync(model, cancellationToken);

			if (!result.Success)
			{
				ModelState.AddModelError(string.Empty, result.ErrorMessage ?? "Create Product Failed...");
				return View(model);
			}

			TempData["Success"] = "Product Create Successfully...";

			return RedirectToAction(nameof(Index));
		}

		public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
		{
			var product = await _productApiClient.GetByIdAsync(id, cancellationToken);

			if (product == null)
			{
				return NotFound();
			}

			var model = new EditProductVM
			{
				Id = id,
				ProductName = product.ProductName,
				ProductCode = product.ProductCode,
				ProductModel = product.ProductModel,
				ProductDesc = product.ProductDesc,
				ProductPrice = product.ProductPrice,
				IsAvailable = product.IsAvailable,
				ShowInSlider = product.ShowInSlider
			};

			return View(model);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(EditProductVM model, CancellationToken cancellationToken)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			var result = await _productApiClient.UpdateAsync(model.Id, model, cancellationToken);

			if (!result.Success)
			{
				ModelState.AddModelError(string.Empty, result.ErrorMessage ?? "Edit Product Failed...");
				return View(model);
			}

			TempData["Success"] = "Product Edit Successfully...";

			return RedirectToAction(nameof(Index));
		}

		[HttpGet]
		public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
		{
			var product = await _productApiClient.GetByIdAsync(id, cancellationToken);

			if (product == null)
			{
				return NotFound();
			}

			return View(product);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirm(int id, CancellationToken cancellationToken)
		{
			var result = await _productApiClient.DeleteAsync(id, cancellationToken);

			if (!result.Success)
			{
				TempData["Error"] = result.ErrorMessage ?? "Product Delete Failed...";
				return RedirectToAction(nameof(Index));
			}

			TempData["Success"] = "Product Delete Successfully...";

			return RedirectToAction(nameof(Index));
		}
	}
}
