namespace ShoesShop.MVC.Models.Galleries
{
	public sealed class GalleryImageVM
	{
		public int Id { get; set; }
		public int ProductId { get; set; }
		public string ImageName { get; set; } = string.Empty;
		public bool IsMain { get; set; }
		public DateTime CreateDate { get; set; }
	}
}
