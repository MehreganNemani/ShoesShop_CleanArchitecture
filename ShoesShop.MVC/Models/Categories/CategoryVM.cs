namespace ShoesShop.MVC.Models.Categories
{
	public class CategoryVM
	{
        public int Id { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public int Code { get; set; }
        public int? ParentId { get; set; }
        public string? ParentName { get; set; }
        public DateTime CreateDate { get; set; }
        
    }
}
