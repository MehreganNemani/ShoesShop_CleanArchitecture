namespace ShoesShop.MVC.Models.Categories
{
	public class CategoryDtoForProductCategory
	{
		public int Id { get; set; }
		public string CategoryName { get; set; } = string.Empty;
		public int? ParentId { get; set; }
	}
}
