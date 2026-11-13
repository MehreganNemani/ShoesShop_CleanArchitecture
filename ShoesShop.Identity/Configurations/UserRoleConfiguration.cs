using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ShoesShop.Identity.Configurations
{
	public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
	{
		public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
		{
			builder.HasData(new IdentityUserRole<string>
			{
				UserId = "e779d562-4dc5-4384-a113-9a6f5ea86163",
				RoleId = "058390da-2750-4948-b43e-3590296bb5ef"
			}, new IdentityUserRole<string>
			{
				UserId = "bd84b642-66a6-4e44-ab4b-8a7670f2dfd0",
				RoleId = "40b8214d-4633-4ce4-92f0-c93db9827805"
			});
		}
	}
}
