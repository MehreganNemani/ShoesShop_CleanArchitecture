namespace ShoesShop.MVC.Models.ProductTags
{
	public class ProductTagDto
	{
		public int ProductID { get; set; }

		public int TagID { get; set; }

		public string TagTitle { get; set; } = string.Empty;
	}
}
