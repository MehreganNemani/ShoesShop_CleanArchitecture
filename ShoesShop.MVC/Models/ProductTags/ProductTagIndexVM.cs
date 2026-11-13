using ShoesShop.MVC.Models.Products;
using ShoesShop.MVC.Models.Tags;

namespace ShoesShop.MVC.Models.ProductTags
{
	public class ProductTagIndexVM
	{
		public int ProductID { get; set; }

		public ProductInfoDto? ProductInfo { get; set; }

		public List<ProductTagDto> ProductTags { get; set; } = new();

		public List<TagVM> AvailableTags { get; set; } = new();
	}
}
