using Microsoft.EntityFrameworkCore;
using ShoesShop.Domain.Entities.Products;

namespace ShoesShop.Persistance.Configurations.SizeConfiguration
{
	public static class SizeConfiguration
	{
		public static void Seed(this ModelBuilder modelBuilder)
		{
			var seedData = new DateTime(2026, 10, 3, 0, 0, 0, DateTimeKind.Utc);
			modelBuilder.Entity<Size>().HasData(new Size()
			{
				ID = 1,
				SizeNumber = 35,
				CreateDate = seedData,
				IsDeleted = false
			},
			new Size()
			{
				ID = 2,
				SizeNumber = 36,
				CreateDate = seedData,
				IsDeleted = false
			},
			new Size()
			{
				ID = 3,
				SizeNumber = 37,
				CreateDate = seedData,
				IsDeleted = false
			},
			new Size()
			{
				ID = 4,
				SizeNumber = 38,
				CreateDate = seedData,
				IsDeleted = false
			},
			new Size()
			{
				ID = 5,
				SizeNumber = 39,
				CreateDate = seedData,
				IsDeleted = false
			},
			new Size()
			{
				ID = 6,
				SizeNumber = 40,
				CreateDate = seedData,
				IsDeleted = false
			},
			new Size()
			{
				ID = 7,
				SizeNumber = 41,
				CreateDate = seedData,
				IsDeleted = false
			},
			new Size()
			{
				ID = 8,
				SizeNumber = 42,
				CreateDate = seedData,
				IsDeleted = false
			},
			new Size()
			{
				ID = 9,
				SizeNumber = 43,
				CreateDate = seedData,
				IsDeleted = false
			},
			new Size()
			{
				ID = 10,
				SizeNumber = 44,
				CreateDate = seedData,
				IsDeleted = false
			},
			new Size()
			{
				ID = 11,
				SizeNumber = 45,
				CreateDate = seedData,
				IsDeleted = false
			});

		}
	}
}
