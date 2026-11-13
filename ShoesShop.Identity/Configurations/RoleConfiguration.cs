using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ShoesShop.Identity.Configurations
{
	public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
	{
		public void Configure(EntityTypeBuilder<IdentityRole> builder)
		{
			builder.HasData(
					new IdentityRole
					{
						Id = "058390da-2750-4948-b43e-3590296bb5ef",
						Name = "Admin",
						NormalizedName = "ADMIN",

					},
					new IdentityRole
					{
						Id = "40b8214d-4633-4ce4-92f0-c93db9827805",
						Name = "User",
						NormalizedName = "USER",
					}
				);
		}
	}
}
