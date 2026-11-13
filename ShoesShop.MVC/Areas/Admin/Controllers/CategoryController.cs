using Microsoft.AspNetCore.Mvc;
using ShoesShop.MVC.Models.Categories;
using ShoesShop.MVC.Services.Interfaces;

namespace ShoesShop.MVC.Areas.Admin.Controllers
{
	[Area("Admin")]
	[AutoValidateAntiforgeryToken]
	[RequireHttps]
	public class CategoryController : Controller
	{
		private readonly ICategoryApiClient _categoryApiClient;

		public CategoryController(ICategoryApiClient categoryApiClient)
		{
			_categoryApiClient = categoryApiClient;
		}

		[HttpGet]
		public async Task<IActionResult> Index(string? search, CancellationToken cancellationToken)
		{
			var result = await _categoryApiClient.GetAllAsync(search, cancellationToken);

			ViewBag.Search = search;

			return View(result);
		}

		[HttpGet]
		public async Task<IActionResult> Create(CancellationToken cancellationToken)
		{
			var options = await _categoryApiClient.GetOptionsAsync(cancellationToken);

			ViewBag.ParentCategories = options;

			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(CreateCategoryVM model, CancellationToken cancellationToken)
		{
			if (!ModelState.IsValid)
			{
				var options = await _categoryApiClient.GetOptionsAsync(cancellationToken);

				ViewBag.ParentCategories = options;

				return View(model);
			}

			var result = await _categoryApiClient.CreateAsync(new CreateCategoryVM
			{
				CategoryName = model.CategoryName,
				ParentId = model.ParentId,
				Code = model.Code
			}, cancellationToken);

			if (!result.Success)
			{
				ModelState.AddModelError(string.Empty, result.ErrorMessage ?? "An Error Occurred...");

				var options = await _categoryApiClient.GetOptionsAsync(cancellationToken);

				ViewBag.ParentCategories = options;

				return View(model);
			}

			TempData["Success"] = "Category Create Successfully...";

			return RedirectToAction(nameof(Index));
		}

		[HttpGet]
		public async Task<IActionResult> Edit(int Id, CancellationToken cancellationToken)
		{
			var category = await _categoryApiClient.GetByIdAsync(Id, cancellationToken);

			if (category == null)
			{
				return NotFound();
			}

			var categories = await _categoryApiClient.GetAvailableParentsAsync(Id, cancellationToken);

			var model = new CategoryEditVM
			{
				Id = category.Id,
				CategoryName = category.CategoryName,
				ParentId = category.ParentId,
				Code = category.Code,
				Categories = categories
			};

			ViewBag.ParentCategories = categories;

			return View(model);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(CategoryEditVM model, CancellationToken cancellationToken)
		{
			if (!ModelState.IsValid)
			{
				ViewBag.ParentCategories = await _categoryApiClient.GetAvailableParentsAsync(model.Id, cancellationToken);
				return View(model);
			}

			var request = new UpdateCategoryVM
			{
				CategoryName = model.CategoryName,
				ParentId = model.ParentId,
				Code = model.Code
			};

			var result = await _categoryApiClient.UpdateAsync(model.Id, request, cancellationToken);

			if (!result.Success)
			{
				ModelState.AddModelError(string.Empty, result.ErrorMessage ?? "An Error Occurred...");

				ViewBag.ParentCategories = await _categoryApiClient.GetAvailableParentsAsync(model.Id, cancellationToken);

				return View(model);
			}

			TempData["Success"] = "Category Edit Successfully...";

			return RedirectToAction(nameof(Index));
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Delete(int Id, CancellationToken cancellationToken)
		{
			var result = await _categoryApiClient.DeleteAsync(Id, cancellationToken);

			if (!result.Success)
			{
				TempData["Error"] = result.ErrorMessage ?? "An Error Occurred...";

				return RedirectToAction(nameof(Index));
			}

			TempData["Success"] = "Category Delete Successfully...";
			return RedirectToAction(nameof(Index));
		}
	}
}
