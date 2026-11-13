using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShoesShop.Domain.Entities.Products;

namespace ShoesShop.Persistance.Configurations.ProductConfiguration
{
	public class ProductConfiguration : IEntityTypeConfiguration<Product>
	{
		public void Configure(EntityTypeBuilder<Product> builder)
		{
			builder.ToTable("Products");
			builder.HasKey(p => p.ID);
			builder.Property(p => p.ProductName).HasMaxLength(100).IsRequired();
			builder.HasIndex(p => p.ProductName).IsUnique().HasFilter("[IsDeleted] = 0");
			builder.Property(p => p.ProductDesc).IsRequired();
			builder.Property(p => p.ProductCode).HasMaxLength(6).IsRequired();
			builder.HasIndex(p => p.ProductCode).IsUnique().HasFilter("[IsDeleted] = 0");
			builder.Property(p => p.ProductModel).HasMaxLength(8).IsRequired();
			builder.HasIndex(p => p.ProductModel).IsUnique().HasFilter("[IsDeleted] = 0");
			builder.Property(p => p.ProductPrice).HasColumnType("decimal(18,2)");
		}
	}
}
