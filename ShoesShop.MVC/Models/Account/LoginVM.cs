using System.ComponentModel.DataAnnotations;

namespace ShoesShop.MVC.Models.Account
{
	public class LoginVM
	{
		[Required(ErrorMessage = "Email Is Required...")]
		[EmailAddress(ErrorMessage = "Invalid Email Address...")]
		public string Email { get; set; } = string.Empty;

		[Required(ErrorMessage = "Password Is Required...")]
		[DataType(DataType.Password)]
		public string Password { get; set; } = string.Empty;

        public bool RememberMe { get; set; }
    }
}
