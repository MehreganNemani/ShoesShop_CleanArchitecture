using Microsoft.AspNetCore.Mvc;
using ShoesShop.MVC.Models.Color;
using ShoesShop.MVC.Services.Interfaces;

namespace ShoesShop.MVC.Areas.Admin.Controllers
{
	[Area("Admin")]
	[AutoValidateAntiforgeryToken]
	[RequireHttps]
	public class ColorController : Controller
	{
		private readonly IColorApiClient _colorApiClient;
		public ColorController(IColorApiClient colorApiClient)
		{
			_colorApiClient = colorApiClient;
		}

		public async Task<IActionResult> Index(string? search, int pageNumber, int pageSize, CancellationToken cancellationToken = default)
		{
			var result = await _colorApiClient.GetPagedAsync(search, pageNumber, pageSize, cancellationToken);

			ViewBag.Serach = search;

			return View(result);
		}

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(CreateColorVM model, CancellationToken cancellationToken)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			var result = await _colorApiClient.CreateAsync(model, cancellationToken);

			if (!result.Success)
			{
				ModelState.AddModelError(string.Empty, result.ErrorMessage ?? "Create Color Failed...");
				return View(model);
			}

			TempData["Success"] = "Color Create Successfully...";

			return RedirectToAction(nameof(Index));
		}

		public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
		{
			var color = await _colorApiClient.GetByIdAsync(id, cancellationToken);

			if (color == null)
			{
				return NotFound();
			}

			var model = new EditColorVM
			{
				Id = id,
				ColorName = color.ColorName
			};

			return View(model);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(EditColorVM model, CancellationToken cancellationToken)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			var result = await _colorApiClient.UpdateAsync(model.Id, model, cancellationToken);

			if (!result.Success)
			{
				ModelState.AddModelError(string.Empty, result.ErrorMessage ?? "Edit Color Failed...");
				return View(model);
			}

			TempData["Success"] = "Color Edit Successfully...";

			return RedirectToAction(nameof(Index));
		}

		[HttpGet]
		public async Task<IActionResult> Delete(int id)
		{
			var color = await _colorApiClient.GetByIdAsync(id);

			if (color == null)
			{
				return NotFound();
			}

			return View(color);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
		{
			var result = await _colorApiClient.DeleteAsync(id, cancellationToken);

			if (!result.Success)
			{
				TempData["Error"] = result.ErrorMessage ?? "Color Delete Failed...";
				return RedirectToAction(nameof(Index));
			}

			TempData["Success"] = "Color Delete Successfully...";

			return RedirectToAction(nameof(Index));
		}

	}
}
