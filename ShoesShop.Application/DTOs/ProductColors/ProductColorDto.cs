namespace ShoesShop.Application.DTOs.ProductColors
{
	public sealed class ProductColorDto
	{
		public int ProductID { get; set; }

		public int ColorID { get; set; }

		public string ColorName { get; set; } = string.Empty;
	}
}
