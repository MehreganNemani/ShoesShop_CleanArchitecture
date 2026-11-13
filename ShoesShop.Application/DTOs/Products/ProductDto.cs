using ShoesShop.Application.DTOs.Common;

namespace ShoesShop.Application.DTOs.Products
{
	public sealed class ProductDto : BaseDto
	{
		public string ProductName { get; set; } = string.Empty;
		public string ProductCode { get; set; } = string.Empty;
		public string ProductModel { get; set; } = string.Empty;
		public decimal ProductPrice { get; set; }
		public string ProductDesc { get; set; } = string.Empty;
		public bool ShowInSlider { get; set; }
		public bool IsAvailable { get; set; }
	}
}
