using ShoesShop.Domain.Entities.Common;

namespace ShoesShop.Domain.Entities.Products
{
	public class Category : BaseEntity
	{
		public string CategoryName { get; set; } = string.Empty;

		public int? ParentID { get; set; }

		public int Code { get; set; }

		#region Relations
		public ICollection<ProductCategory> ProductCategories { get; set; } = new List<ProductCategory>();

		public Category? Parent { get; set; }

		public ICollection<Category> Children { get; set; } = new List<Category>();

		#endregion
	}
}
