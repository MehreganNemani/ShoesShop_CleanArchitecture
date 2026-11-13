using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShoesShop.Domain.Entities.Products;

namespace ShoesShop.Persistance.Configurations.TagConfiguration
{
	public class TagConfiguration : IEntityTypeConfiguration<Tag>
	{
		public void Configure(EntityTypeBuilder<Tag> builder)
		{
			builder.ToTable("Tags");
			builder.HasKey(t => t.ID);
			builder.Property(t => t.TagTitle).HasMaxLength(50).IsRequired();
			builder.HasIndex(t => t.TagTitle).IsUnique().HasFilter("[IsDeleted] = 0");
		}
	}
}
