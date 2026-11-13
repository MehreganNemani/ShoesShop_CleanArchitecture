namespace ShoesShop.MVC.Models.Galleries
{
	public sealed class GalleryIndexVM
	{
		public int ProductId { get; set; }

		public string ProductName { get; set; } = string.Empty;
		public string ProductCode { get; set; } = string.Empty;
		public string ImageBaseUrl { get; set; } = string.Empty;
		public List<GalleryImageVM> Images { get; set; } = new();
	}
}
