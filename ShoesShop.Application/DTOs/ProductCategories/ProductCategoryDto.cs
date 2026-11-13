namespace ShoesShop.Application.DTOs.ProductCategories
{
	public sealed class ProductCategoryDto
	{
		public int ProductId { get; set; }

		public int CategoryId { get; set; }

		public string CategoryName { get; set; } = string.Empty;
	}
}
