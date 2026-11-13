namespace ShoesShop.MVC.Models
{
	public class ApiValidationErrorResponse
	{
		public Dictionary<string, string[]> Errors { get; set; } = new();
	}
}
