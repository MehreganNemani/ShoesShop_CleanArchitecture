using ShoesShop.Domain.Entities.Common;

namespace ShoesShop.Domain.Entities.Products
{
	public class Product : BaseEntity
	{
		public string ProductName { get; set; } = string.Empty;
		public string ProductCode { get; set; } = string.Empty;
		public string ProductModel { get; set; } = string.Empty;
		public decimal ProductPrice { get; set; }
		public string ProductDesc { get; set; } = string.Empty;
        public bool ShowInSlider { get; set; }
        public bool IsAvailable { get; set; }

        #region Relations

        public ICollection<Gallery> Galleries { get; set; }	= new List<Gallery>();
		public ICollection<ProductColor> ProductColors { get; set; } = new List<ProductColor>();
		public ICollection<ProductSize> ProductSizes { get; set; } = new List<ProductSize>();
		public ICollection<ProductCategory> ProductCategories { get; set; } = new List<ProductCategory>();
		public ICollection<ProductTag> ProductTags { get; set; } = new List<ProductTag>();
		
		#endregion
	}
}
