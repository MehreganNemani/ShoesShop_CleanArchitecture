using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShoesShop.Domain.Entities.Products;

namespace ShoesShop.Persistance.Configurations.ProductCategoryConfiguration
{
	public class ProductCategoryConfiguration : IEntityTypeConfiguration<ProductCategory>
	{
		public void Configure(EntityTypeBuilder<ProductCategory> builder)
		{
			builder.ToTable("ProductCategory");

			builder.HasKey(pc => pc.ID);

			builder.Property(pc => pc.ProductID).IsRequired();

			builder.Property(pc => pc.CategoryID).IsRequired();

			builder.HasOne(pc => pc.Product)
				  .WithMany(pc => pc.ProductCategories)
				  .HasForeignKey(pc => pc.ProductID)
				  .OnDelete(DeleteBehavior.Restrict);

			builder.HasOne(pc => pc.Category)
				  .WithMany(pc => pc.ProductCategories)
				  .HasForeignKey(pc => pc.CategoryID)
				  .OnDelete(DeleteBehavior.Restrict);

			builder.HasIndex(pc => new
			{
				pc.ProductID,
				pc.CategoryID
			}).IsUnique();
		}
	}
}
