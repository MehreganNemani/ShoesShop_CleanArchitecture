using System.ComponentModel.DataAnnotations;

namespace ShoesShop.MVC.Models.Color
{
    public class CreateColorVM
    {
        [Required]
        [MaxLength(50)]
        public string ColorName { get; set; } = string.Empty;
    }
}
