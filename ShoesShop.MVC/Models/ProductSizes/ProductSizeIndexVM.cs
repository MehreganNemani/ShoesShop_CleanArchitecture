using ShoesShop.MVC.Models.Products;
using ShoesShop.MVC.Models.Sizes;

namespace ShoesShop.MVC.Models.ProductSizes
{
	public class ProductSizeIndexVM
	{
		public int ProductID { get; set; }

		public ProductInfoDto? ProductInfo { get; set; }

		public List<ProductSizeDto> ProductSizes { get; set; } = new();

		public List<SizeVM> AvailableSizes { get; set; } = new();
	}
}
