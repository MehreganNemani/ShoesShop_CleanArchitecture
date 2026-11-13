using System.ComponentModel.DataAnnotations;

namespace ShoesShop.MVC.Models.Account
{
	public class RegisterVM
	{
		[Required]
		public string FirstName { get; set; } = string.Empty;

		[Required]
		public string LastName { get; set; } = string.Empty;

		[Required(ErrorMessage = "Email Is Required...")]
		[EmailAddress(ErrorMessage = "Invalid Email Address...")]
		public string Email { get; set; } = string.Empty;

		[Required(ErrorMessage = "UserName Is Required...")]
		[MinLength(6)]
		public string UserName { get; set; } = string.Empty;

		[Required(ErrorMessage = "Password Is Required...")]
		[MinLength(6)]
		[DataType(DataType.Password)]
		public string Password { get; set; } = string.Empty;

		[Required(ErrorMessage = "Confirm Password Is Required...")]
		[DataType(DataType.Password)]
		[Compare("Password", ErrorMessage = "Password Do Not Match...")]
		public string ConfirmPassword { get; set; } = string.Empty;
	}
}
