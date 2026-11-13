namespace ShoesShop.MVC.Models.Account
{
	public class ApiErrorResponseDto
	{
		public string Message { get; set; }
		public Dictionary<string, string[]> Errors { get; set; }
	}
}
