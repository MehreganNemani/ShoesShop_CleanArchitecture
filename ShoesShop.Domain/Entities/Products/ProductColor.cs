using ShoesShop.Domain.Entities.Common;

namespace ShoesShop.Domain.Entities.Products
{
	public class ProductColor : BaseEntity
	{
		public int ColorID { get; set; }
		public int ProductID { get; set; }

		#region Relations

		public Color? Color { get; set; }

		public Product? Product { get; set; }


		#endregion
	}
}
