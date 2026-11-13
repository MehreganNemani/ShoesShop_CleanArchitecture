using Microsoft.AspNetCore.Mvc;
using ShoesShop.MVC.Models.Galleries;
using ShoesShop.MVC.Services.Interfaces;

namespace ShoesShop.MVC.Areas.Admin.Controllers
{
	[Area("Admin")]
	[AutoValidateAntiforgeryToken]
	[RequireHttps]
	public class GalleryController : Controller
	{
		private readonly IGalleryApiClient _galleryApiClient;
		private readonly IProductApiClient _productApiClient;
		private readonly IConfiguration _configuration;

		public GalleryController(IGalleryApiClient galleryApiClient, IProductApiClient productApiClient, IConfiguration configuration)
		{
			_galleryApiClient = galleryApiClient;
			_productApiClient = productApiClient;
			_configuration = configuration;
		}

		public async Task<IActionResult> Index(int productId)
		{
			if (productId <= 0)
			{
				return BadRequest();
			}

			try
			{
				var images = await _galleryApiClient.GetByProductIdAsync(productId);

				var product = await _productApiClient.GetByIdAsync(productId);

				var model = new GalleryIndexVM
				{
					ProductId = productId,
					ProductCode = product.ProductCode,
					ProductName = product.ProductName,
					Images = images,
					ImageBaseUrl = _configuration["ApiSettings:GalleryImageBaseUrl"] ?? string.Empty
				};

				return View(model);
			}
			catch (HttpRequestException)
			{
				TempData["Error"] = "We Could Not Connect To Api...";

				return View(new GalleryIndexVM
				{
					ProductId = productId,
					Images = new List<GalleryImageVM>(),
					ImageBaseUrl = _configuration["ApiSettings:GalleryImageBaseUrl"] ?? string.Empty
				});
			}
			catch (TaskCanceledException)
			{
				TempData["Error"] = "We Could Not Receive Api Response , Try Again...";

				return View(new GalleryIndexVM
				{
					ProductId = productId,
					Images = new List<GalleryImageVM>(),
					ImageBaseUrl = _configuration["ApiSettings:GalleryImageBaseUrl"] ?? string.Empty
				});

			}
		}

		[HttpGet]
		public IActionResult Upload(int productId)
		{
			if (productId <= 0)
			{
				return BadRequest();
			}

			var product = _productApiClient.GetByIdAsync(productId);



			return View(new GalleryIndexVM { ProductId = productId, ProductName = product.Result.ProductName, ProductCode = product.Result.ProductCode });
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Upload(int productId, List<IFormFile> images)
		{
			if (productId <= 0)
			{
				ModelState.AddModelError(string.Empty, "ID Is Not Valid...");
			}

			if (images is null || images.Count == 0)
			{
				ModelState.AddModelError(nameof(images), "You Have to Select a Photo...");
			}

			if (!ModelState.IsValid)
			{
				return View(new GalleryIndexVM
				{
					ProductId = productId,
					Images = new List<GalleryImageVM>(),
					ImageBaseUrl = _configuration["ApiSettings:GalleryImageBaseUrl"] ?? string.Empty
				});
			}

			try
			{
				var success = await _galleryApiClient.AddImageAsync(productId, images);

				if (!success)
				{
					ModelState.AddModelError(string.Empty, "Image Upload Failed By Api...");

					return View(new GalleryIndexVM
					{
						ProductId = productId,
						Images = new List<GalleryImageVM>(),
						ImageBaseUrl = _configuration["ApiSettings:GalleryImageBaseUrl"] ?? string.Empty
					});
				}

				TempData["Success"] = "Images Save Successfully...";

				return RedirectToAction(nameof(Index), new { productId });

			}
			catch (HttpRequestException)
			{
				ModelState.AddModelError(string.Empty, "We Could Not Connect To Api...");

				return View(new GalleryIndexVM
				{
					ProductId = productId,
					Images = new List<GalleryImageVM>(),
					ImageBaseUrl = _configuration["ApiSettings:GalleryImageBaseUrl"] ?? string.Empty
				});
			}
			catch (TaskCanceledException)
			{
				ModelState.AddModelError(string.Empty, "Time For Image Upload Runnig Out...");

				return View(new GalleryIndexVM
				{
					ProductId = productId,
					Images = new List<GalleryImageVM>(),
					ImageBaseUrl = _configuration["ApiSettings:GalleryImageBaseUrl"] ?? string.Empty
				});
			}
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Delete(int id, int productId)
		{
			try
			{
				var success = await _galleryApiClient.DeleteAsync(id);

				if (!success)
				{
					TempData["Error"] = "Remove Image Failed...";
				}
				else
				{
					TempData["Success"] = "Image Delete Successfully...";
				}

			}
			catch (HttpRequestException)
			{
				TempData["Error"] = "We Could Not Connect To Api...";
			}
			catch (TaskCanceledException)
			{
				TempData["Error"] = "Time For Image Upload Runnig Out...";
			}

			return RedirectToAction(nameof(Index), new { productId });
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> SetMain(int id, int productId, CancellationToken cancellationToken)
		{
			var success = await _galleryApiClient.SetMainAsync(id, cancellationToken);
			if (!success)
			{
				TempData["Error"] = "Set Main Image Failed...";
			}

			return RedirectToAction(nameof(Index), new { productId });
		}
	}
}
