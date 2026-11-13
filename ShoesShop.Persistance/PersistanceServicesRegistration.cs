using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShoesShop.Application.Common.Interfaces;
using ShoesShop.Persistance.Repositories;

namespace ShoesShop.Persistance
{
	public static class PersistanceServicesRegistration
	{
		public static IServiceCollection ConfigurePersistanceServices(this IServiceCollection services,
			IConfiguration configuration)
		{
			services.AddDbContext<ShoesShop_DbContext>(options =>
			{
				options.UseSqlServer(configuration.GetConnectionString("ShoesShopConnectionString"));
			});

			services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
			services.AddScoped<IUnitOfWork, UnitOfWork>();
			services.AddScoped<IProductRepository, ProductRepository>();
			services.AddScoped<IColorRepository, ColorRepository>();
			services.AddScoped<ITagRepository, TagRepository>();
			services.AddScoped<ICategoryRepository, CategoryRepository>();
			services.AddScoped<IGalleryRepository, GalleryRepository>();
			services.AddScoped<IImageStorageService, ImageStorageService>();

			return services;
		}
	}
}
