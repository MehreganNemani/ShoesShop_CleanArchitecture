namespace ShoesShop.MVC.Models.ProductCategories
{
	public class AddProductCategoryVM
	{
		public int ProductId { get; set; }
		public int ParentCategoryId { get; set; }
		public int ChildCategoryId { get; set; }
	}
}
