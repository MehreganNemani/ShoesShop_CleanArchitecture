using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShoesShop.Domain.Entities.Products;

namespace ShoesShop.Persistance.Configurations.CategoryConfiguration
{
	public class CategoryConfiguration : IEntityTypeConfiguration<Category>
	{
		public void Configure(EntityTypeBuilder<Category> builder)
		{
			builder.ToTable("Category");
			builder.HasKey(c => c.ID);
			builder.Property(c => c.CategoryName).HasMaxLength(50).IsRequired();
			builder.Property(c => c.ParentID).IsRequired(false);
			builder.Property(c => c.Code).IsRequired();
			builder.HasOne(c => c.Parent)
				   .WithMany(c => c.Children)
				   .HasForeignKey(c => c.ParentID)
				   .OnDelete(DeleteBehavior.Restrict);
		}
	}
}
