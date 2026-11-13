using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShoesShop.Domain.Entities.Products;

namespace ShoesShop.Persistance.Configurations.ProductTagConfiguration
{
	public class ProductTagConfiguration : IEntityTypeConfiguration<ProductTag>
	{
		public void Configure(EntityTypeBuilder<ProductTag> builder)
		{
			builder.ToTable("ProductTags");

			builder.HasKey(pt => pt.ID);

			builder.Property(pt => pt.ProductID).IsRequired();

			builder.Property(pt => pt.TagID).IsRequired();

			builder.HasOne(pt => pt.Product)
				  .WithMany(pt => pt.ProductTags)
				  .HasForeignKey(pt => pt.ProductID)
				  .OnDelete(DeleteBehavior.Restrict);

			builder.HasOne(pt => pt.Tag)
				  .WithMany(pt => pt.ProductTags)
				  .HasForeignKey(pt => pt.TagID)
				  .OnDelete(DeleteBehavior.Restrict);
		}
	}
}
