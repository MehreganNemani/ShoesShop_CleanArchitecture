using System.ComponentModel.DataAnnotations;

namespace ShoesShop.MVC.Models.Products
{
	public class CreateProductVM
	{

		[Required]
		[MaxLength(100)]
		public string ProductName { get; set; } = string.Empty;

		[Required]
		[MaxLength(6)]
		public string ProductCode { get; set; } = string.Empty;

		[Required]
		[MaxLength(8)]
		public string ProductModel { get; set; } = string.Empty;

		[Required]
		public decimal ProductPrice { get; set; }

		[Required]
		public string ProductDesc { get; set; } = string.Empty;
		public bool ShowInSlider { get; set; }
		public bool IsAvailable { get; set; }
	}
}
