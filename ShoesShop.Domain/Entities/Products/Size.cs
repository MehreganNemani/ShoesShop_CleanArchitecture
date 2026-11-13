using ShoesShop.Domain.Entities.Common;

namespace ShoesShop.Domain.Entities.Products
{
	public class Size : BaseEntity
	{
		public int SizeNumber { get; set; } 

		#region Relations
		public ICollection<ProductSize> ProductSizes { get; set; } = new List<ProductSize>();

		#endregion
	}
}
