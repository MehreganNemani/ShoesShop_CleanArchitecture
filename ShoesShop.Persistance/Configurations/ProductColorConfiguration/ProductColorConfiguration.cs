using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShoesShop.Domain.Entities.Products;

namespace ShoesShop.Persistance.Configurations.ProductColorConfiguration
{
	public class ProductColorConfiguration : IEntityTypeConfiguration<ProductColor>
	{
		public void Configure(EntityTypeBuilder<ProductColor> builder)
		{
			builder.ToTable("ProductColors");

			builder.HasKey(pc => pc.ID);

			builder.Property(pc => pc.ProductID).IsRequired();

			builder.Property(pc => pc.ColorID).IsRequired();

			builder.HasOne(pc => pc.Product)
				  .WithMany(pc => pc.ProductColors)
				  .HasForeignKey(pc => pc.ProductID)
				  .OnDelete(DeleteBehavior.Restrict);

			builder.HasOne(pc => pc.Color)
				  .WithMany(pc => pc.ProductColors)
				  .HasForeignKey(pc => pc.ColorID)
				  .OnDelete(DeleteBehavior.Restrict);
		}
	}
}
