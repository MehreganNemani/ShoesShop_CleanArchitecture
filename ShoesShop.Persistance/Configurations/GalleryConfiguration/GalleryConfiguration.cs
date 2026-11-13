using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShoesShop.Domain.Entities.Products;

namespace ShoesShop.Persistance.Configurations.GalleryConfiguration
{
	public class GalleryConfiguration : IEntityTypeConfiguration<Gallery>
	{
		public void Configure(EntityTypeBuilder<Gallery> builder)
		{
			builder.ToTable("Gallery");

			builder.HasKey(g => g.ID);

			builder.Property(g => g.ImageName).HasMaxLength(200).IsRequired();

			builder.Property(g => g.IsMain).IsRequired();

			builder.HasOne(g => g.Product).WithMany(g => g.Galleries).HasForeignKey(g => g.ProductID).OnDelete(DeleteBehavior.Restrict);

			builder.HasIndex(g => g.ProductID).HasDatabaseName("IX_Gallery_ProductId");

			builder.HasIndex(g => g.ProductID).IsUnique().HasFilter("[IsMain] = 1").HasDatabaseName("UX_Gallery_OneMainPerProduct");
		}
	}
}
