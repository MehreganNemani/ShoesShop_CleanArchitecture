namespace ShoesShop.MVC.Models.Products
{
	public class ProductVM
	{
		public int Id { get; set; }
		public string ProductName { get; set; } = string.Empty;
		public string ProductCode { get; set; } = string.Empty;
		public string ProductModel { get; set; } = string.Empty;
		public decimal ProductPrice { get; set; }
		public string ProductDesc { get; set; } = string.Empty;
		public bool ShowInSlider { get; set; }
		public bool IsAvailable { get; set; }
		public DateTime CreateDate { get; set; }
		public DateTime? UpdatedDate { get; set; }
	}
}
