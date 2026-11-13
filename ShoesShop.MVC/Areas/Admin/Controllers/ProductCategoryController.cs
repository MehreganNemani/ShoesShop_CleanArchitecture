using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShoesShop.MVC.Models.ProductCategories;
using ShoesShop.MVC.Services.Interfaces;

namespace ShoesShop.MVC.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class ProductCategoryController : Controller
	{
		private readonly IProductCategoryApiClient _apiClient;

		public ProductCategoryController(IProductCategoryApiClient apiClient)
		{
			_apiClient = apiClient;
		}

		[HttpGet]
		public async Task<IActionResult> Index(int productId)
		{
			if (productId <= 0)
			{
				return BadRequest();
			}

			var parentCategories = await _apiClient.GetParentCategoriesAsync();

			var productCategories = await _apiClient.GetProductCategoriesAsync(productId);

			var model = new ProductCategoryVM
			{
				ProductId = productId,
				ParentCategories = parentCategories.Select(pc => new SelectListItem
				{
					Value = pc.Id.ToString(),
					Text = pc.CategoryName
				}).ToList(),
				ProductCategories = productCategories

			};

			return View(model);
		}

		[HttpGet]
		public async Task<IActionResult> GetChildCategories(int parentId)
		{
			if (parentId <= 0)
			{
				return BadRequest();
			}

			var categories = await _apiClient.GetChildCategoriesAsync(parentId);

			return Json(categories.Select(c => new
			{
				id = c.Id,
				Name = c.CategoryName
			}));
		}

		[HttpPost]
		public async Task<IActionResult> Add([FromBody] AddProductCategoryVM model)
		{
			if (model.ProductId <= 0 || model.ParentCategoryId <= 0 || model.ChildCategoryId <= 0)
			{
				return BadRequest("Your Information Is False...");
			}

			var result = await _apiClient.AddProductCategoryAsync(model);

			if (!result.Success)
			{
				return BadRequest(result.Message);
			}

			return Ok(result.Message);
		}

		[HttpPost]
		public async Task<IActionResult> Remove(int productId, int categoryId)
		{
			if (productId <= 0 || categoryId <= 0)
			{
				return BadRequest();
			}

			var result = await _apiClient.RemoveProductCategoryAsync(productId, categoryId);

			if (!result)
			{
				return BadRequest("Remove Action Failed...");
			}

			return Ok();
		}

	}
}
