using System.ComponentModel.DataAnnotations;

namespace ShoesShop.MVC.Models.Tags
{
	public class CreateTagVM
	{
		[Required]
		[MaxLength(50)]
		public string TagTitle { get; set; } = string.Empty;
	}
}
