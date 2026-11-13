using Microsoft.AspNetCore.Mvc.Rendering;

namespace ShoesShop.MVC.Models.ProductCategories
{
	public class ProductCategoryVM
	{
		public int ProductId { get; set; }

		public int ParentCategoryId { get; set; }
		public int ChildCategoryId { get; set; }

		public List<SelectListItem> ParentCategories { get; set; } = new List<SelectListItem>();

		public List<SelectListItem> ChildCategories { get; set; } = new List<SelectListItem>();

		public List<ProductCategoryDto> ProductCategories { get; set; } = new List<ProductCategoryDto>();

	}
}
