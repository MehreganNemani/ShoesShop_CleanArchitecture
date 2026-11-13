namespace ShoesShop.MVC.Models.Tags
{
	public class TagVM
	{
		public int Id { get; set; }
		public string TagTitle { get; set; } = string.Empty;
		public DateTime CreateDate { get; set; }
		public DateTime? UpdatedDate { get; set; }
	}
}
