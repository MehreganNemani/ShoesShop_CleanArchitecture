namespace ShoesShop.Domain.Entities.Common
{
    public abstract class BaseEntity
    {
        public int ID { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}