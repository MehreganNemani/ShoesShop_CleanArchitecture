using ShoesShop.Domain.Entities.Common;

namespace ShoesShop.Domain.Entities.Products
{
	public class ProductSize : BaseEntity
	{
		public int SizeID { get; set; }
		public int ProductID { get; set; }

		#region Relations
		public Size? Size { get; set; }
		public Product? Product { get; set; }

		#endregion
	}
}
