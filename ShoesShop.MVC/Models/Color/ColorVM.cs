namespace ShoesShop.MVC.Models.Color
{
    public class ColorVM
    {
        public int Id { get; set; }
        public string ColorName { get; set; } = string.Empty;
        public DateTime CreateDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
