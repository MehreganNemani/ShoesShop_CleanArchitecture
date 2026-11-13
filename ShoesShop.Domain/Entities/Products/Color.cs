using ShoesShop.Domain.Entities.Common;

namespace ShoesShop.Domain.Entities.Products
{
    public class Color : BaseEntity
    {
        public string ColorName { get;  set; } = string.Empty;

        #region Relations

        public ICollection<ProductColor> ProductColors { get; set; } = new List<ProductColor>();

        #endregion
    }
}
