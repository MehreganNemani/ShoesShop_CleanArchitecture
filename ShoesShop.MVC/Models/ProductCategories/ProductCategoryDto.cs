namespace ShoesShop.MVC.Models.ProductCategories
{
	public class ProductCategoryDto
	{
		public int ProductId { get; set; }

		public int CategoryId { get; set; }

		public string CategoryName { get; set; } = string.Empty;
	}
}
