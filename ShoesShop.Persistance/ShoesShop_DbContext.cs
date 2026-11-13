using Microsoft.EntityFrameworkCore;
using ShoesShop.Domain.Entities.Products;
using ShoesShop.Persistance.Configurations.SizeConfiguration;

namespace ShoesShop.Persistance
{
	public class ShoesShop_DbContext : DbContext
	{
		public ShoesShop_DbContext(DbContextOptions<ShoesShop_DbContext> options) : base(options)
		{

		}

		#region Entities

		public DbSet<Product> Products => Set<Product>();
		public DbSet<Color> Colors => Set<Color>();
		public DbSet<Tag> Tags => Set<Tag>();
		public DbSet<Size> Sizes => Set<Size>();
		public DbSet<Gallery> Galleries => Set<Gallery>();
		public DbSet<Category> Categories => Set<Category>();
		public DbSet<ProductColor> ProductColors => Set<ProductColor>();
		public DbSet<ProductTag> ProductTags => Set<ProductTag>();
		public DbSet<ProductSize> ProductSizes => Set<ProductSize>();
		public DbSet<ProductCategory> ProductCategories => Set<ProductCategory>();

		#endregion

		#region Set Fluent Api

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfigurationsFromAssembly(typeof(ShoesShop_DbContext).Assembly);
			modelBuilder.Seed();
			base.OnModelCreating(modelBuilder);
		}

		#endregion
	}
}
