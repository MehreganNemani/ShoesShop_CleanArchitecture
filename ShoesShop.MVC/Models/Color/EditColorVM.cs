using System.ComponentModel.DataAnnotations;

namespace ShoesShop.MVC.Models.Color
{
    public class EditColorVM
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string ColorName { get; set; } = string.Empty;
    }
}
