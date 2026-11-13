using ShoesShop.Domain.Entities.Common;

namespace ShoesShop.Domain.Entities.Products
{
	public class Gallery : BaseEntity
	{
		public string ImageName { get; set; } = string.Empty;
		public bool IsMain { get; set; }
		public int ProductID { get; set; }

		#region Relations

		public Product Product { get; set; } = null!;

		#endregion
	}
}
