namespace ShoesShop.Api.Contracts.Categories
{
	public sealed class UpdateCategoryRequest
	{
		public string CategoryName { get; set; } = string.Empty;
		public int Code { get; set; }
		public int? ParentId { get; set; }
	}
}
