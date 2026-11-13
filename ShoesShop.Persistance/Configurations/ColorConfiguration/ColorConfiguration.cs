using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShoesShop.Domain.Entities.Products;

namespace ShoesShop.Persistance.Configurations.ColorConfiguration
{
	public class ColorConfiguration : IEntityTypeConfiguration<Color>
	{
		public void Configure(EntityTypeBuilder<Color> builder)
		{
			builder.ToTable("Colors");
			builder.HasKey(c => c.ID);
			builder.Property(c => c.ColorName).HasMaxLength(50).IsRequired();
			builder.HasIndex(c => c.ColorName).IsUnique().HasFilter("[IsDeleted] = 0");
		}
	}
}
