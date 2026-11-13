using Microsoft.AspNetCore.Mvc;
using ShoesShop.MVC.Models.Tags;
using ShoesShop.MVC.Services.Interfaces;

namespace ShoesShop.MVC.Areas.Admin.Controllers
{
	[Area("Admin")]
	[AutoValidateAntiforgeryToken]
	[RequireHttps]
	public class TagController : Controller
	{
		private readonly ITagApiClient _tagApiClient;
		public TagController(ITagApiClient tagApiClient)
		{
			_tagApiClient = tagApiClient;
		}

		public async Task<IActionResult> Index(string? search, int pageNumber, int pageSize, CancellationToken cancellationToken = default)
		{
			var result = await _tagApiClient.GetPagedAsync(search, pageNumber, pageSize, cancellationToken);

			ViewBag.Serach = search;

			return View(result);
		}

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(CreateTagVM model, CancellationToken cancellationToken)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			var result = await _tagApiClient.CreateAsync(model, cancellationToken);

			if (!result.Success)
			{
				ModelState.AddModelError(string.Empty, result.ErrorMessage ?? "Create Tag Failed...");
				return View(model);
			}

			TempData["Success"] = "Tag Create Successfully...";

			return RedirectToAction(nameof(Index));
		}

		public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
		{
			var tag = await _tagApiClient.GetByIdAsync(id, cancellationToken);

			if (tag == null)
			{
				return NotFound();
			}

			var model = new EditTagVM
			{
				Id = id,
				TagTitle = tag.TagTitle
			};

			return View(model);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(EditTagVM model, CancellationToken cancellationToken)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			var result = await _tagApiClient.UpdateAsync(model.Id, model, cancellationToken);

			if (!result.Success)
			{
				ModelState.AddModelError(string.Empty, result.ErrorMessage ?? "Edit Tag Failed...");
				return View(model);
			}

			TempData["Success"] = "Tag Edit Successfully...";

			return RedirectToAction(nameof(Index));
		}

		[HttpGet]
		public async Task<IActionResult> Delete(int id)
		{
			var tag = await _tagApiClient.GetByIdAsync(id);

			if (tag == null)
			{
				return NotFound();
			}

			return View(tag);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
		{
			var result = await _tagApiClient.DeleteAsync(id, cancellationToken);

			if (!result.Success)
			{
				TempData["Error"] = result.ErrorMessage ?? "Tag Delete Failed...";
				return RedirectToAction(nameof(Index));
			}

			TempData["Success"] = "Tag Delete Successfully...";

			return RedirectToAction(nameof(Index));
		}

	}
}
