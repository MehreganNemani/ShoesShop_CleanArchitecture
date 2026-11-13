using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ShoesShop.Identity.Configurations;
using ShoesShop.Identity.Models;

namespace ShoesShop.Identity
{
	public class ShoesShopIdentityDbContext : IdentityDbContext<ApplicationUser>
	{
		public ShoesShopIdentityDbContext(DbContextOptions<ShoesShopIdentityDbContext> options) : base(options)
		{

		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);
			builder.ApplyConfiguration(new RoleConfiguration());
			builder.ApplyConfiguration(new UserConfiguration());
			builder.ApplyConfiguration(new UserRoleConfiguration());
		}
	}
}
