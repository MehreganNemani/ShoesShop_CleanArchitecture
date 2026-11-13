namespace ShoesShop.Api.Contracts.Gallery
{
	public sealed class GalleryImageDto
	{
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ImageName { get; set; } = string.Empty;
        public bool IsMain { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
