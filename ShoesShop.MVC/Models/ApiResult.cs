namespace ShoesShop.MVC.Models
{
	public class ApiResult
	{
		public bool Success { get; init; }

		public string? ErrorMessage { get; init; }

		public static ApiResult Ok()
		{
			return new ApiResult { Success = true };
		}

		public static ApiResult Fail(string message)
		{
			return new ApiResult { Success = false, ErrorMessage = message };
		}
	}
}
