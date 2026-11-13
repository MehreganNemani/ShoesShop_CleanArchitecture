using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShoesShop.Identity.Models;

namespace ShoesShop.Identity.Configurations
{
	public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
	{
		public void Configure(EntityTypeBuilder<ApplicationUser> builder)
		{
			var hasher = new PasswordHasher<ApplicationUser>();
			builder.HasData(
				 new ApplicationUser
				 {
					 Id = "e779d562-4dc5-4384-a113-9a6f5ea86163",
					 Email = "mehreganns.website@gmail.com",
					 NormalizedEmail = "MEHREGANNS.WEBSITE@gmail.COM",
					 FirstName = "Mehregan",
					 LastName = "Nemani Sayyadi",
					 UserName = "mehreganns.website@gmail.com",
					 NormalizedUserName = "MEHREGANNS.WEBSITE@gmail.COM",
					 PasswordHash = hasher.HashPassword(null, "Gunners1886"),
					 EmailConfirmed = true
				 },
				  new ApplicationUser
				  {
					  Id = "bd84b642-66a6-4e44-ab4b-8a7670f2dfd0",
					  Email = "jamshidjamshidzadeh@gmail.com",
					  NormalizedEmail = "JAMSHIDJAMSHIDZADEH@gmail.COM",
					  FirstName = "Jamshid",
					  LastName = "Jamshidzadeh",
					  UserName = "jamshidjamshidzadeh@gmail.com",
					  NormalizedUserName = "JAMSHIDJAMSHIDZADEH@gmail.COM",
					  PasswordHash = hasher.HashPassword(null, "JimmyJJ"),
					  EmailConfirmed = true
				  }
				);
		}
	}
}
