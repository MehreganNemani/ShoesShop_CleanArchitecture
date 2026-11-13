namespace ShoesShop.Application.DTOs.Categories
{
	public sealed class CategoryDto
	{
		public int ID { get; init; }
		public string CategoryName { get; init; } = string.Empty;
		public int Code { get; init; }
		public int? ParentID { get; init; }
		public string? ParentName { get; init; }
		public DateTime CreateDate { get; set; }
		public bool IsDeleted { get; set; }
		public DateTime? LastUpdateDate { get; set; }
	}
}
