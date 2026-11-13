using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShoesShop.Domain.Entities.Products;

namespace ShoesShop.Persistance.Configurations.ProductSizeConfiguration
{
	public class ProductSizeConfiguration : IEntityTypeConfiguration<ProductSize>
	{
		public void Configure(EntityTypeBuilder<ProductSize> builder)
		{
			builder.ToTable("ProductSizes");

			builder.HasKey(ps => ps.ID);

			builder.Property(ps => ps.ProductID).IsRequired();

			builder.Property(ps => ps.SizeID).IsRequired();

			builder.HasOne(ps => ps.Product)
				  .WithMany(ps => ps.ProductSizes)
				  .HasForeignKey(ps => ps.ProductID)
				  .OnDelete(DeleteBehavior.Restrict);

			builder.HasOne(ps => ps.Size)
				  .WithMany(ps => ps.ProductSizes)
				  .HasForeignKey(ps => ps.SizeID)
				  .OnDelete(DeleteBehavior.Restrict);

		}
	}
}
