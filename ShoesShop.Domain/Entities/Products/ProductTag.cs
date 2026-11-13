using ShoesShop.Domain.Entities.Common;

namespace ShoesShop.Domain.Entities.Products
{
	public class ProductTag : BaseEntity
	{
		public int ProductID { get; set; }
		public int TagID { get; set; }

		#region Relations

		public Tag? Tag { get; set; }
		public Product? Product { get; set; }

		#endregion
	}
}
