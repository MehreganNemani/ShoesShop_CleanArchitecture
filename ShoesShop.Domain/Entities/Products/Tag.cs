using ShoesShop.Domain.Entities.Common;

namespace ShoesShop.Domain.Entities.Products
{
	public class Tag : BaseEntity
	{
		public string TagTitle { get; set; } = string.Empty;

		#region Relations

		public ICollection<ProductTag> ProductTags { get; set; } = new List<ProductTag>();

		#endregion
	}
}
