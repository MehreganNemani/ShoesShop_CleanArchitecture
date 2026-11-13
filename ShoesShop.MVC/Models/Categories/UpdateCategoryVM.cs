namespace ShoesShop.MVC.Models.Categories
{
	public sealed class UpdateCategoryVM
	{
        public string CategoryName { get; set; } = string.Empty;
		public int? ParentId { get; set; }
		public int Code { get; set; }
	}
}
