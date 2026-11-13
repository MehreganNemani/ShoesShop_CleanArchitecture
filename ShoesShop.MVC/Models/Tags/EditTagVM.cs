using System.ComponentModel.DataAnnotations;

namespace ShoesShop.MVC.Models.Tags
{
	public class EditTagVM
	{
		public int Id { get; set; }

		[Required]
		[MaxLength(50)]
		public string TagTitle { get; set; } = string.Empty;
	}
}
