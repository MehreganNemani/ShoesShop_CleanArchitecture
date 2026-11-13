using ShoesShop.MVC.Models.Color;
using ShoesShop.MVC.Models.Products;

namespace ShoesShop.MVC.Models.ProductColors
{
	public class ProductColorIndexVM
	{
		public int ProductID { get; set; }

		public ProductInfoDto? ProductInfo { get; set; }

		public List<ProductColorDto> ProductColors { get; set; } = new();

		public List<ColorVM> AvailableColors { get; set; } = new();


	}
}
