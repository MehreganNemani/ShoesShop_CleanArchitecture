using ShoesShop.Domain.Entities.Common;

namespace ShoesShop.Domain.Entities.Products
{
	public class ProductCategory : BaseEntity
	{
		public int CategoryID { get; set; }
		public int ProductID { get; set; }

		#region Relations

		public Category? Category { get; set; }

		public Product? Product { get; set; }


		#endregion

	}
}
