using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using ShoesShop.MVC.Services;
using ShoesShop.MVC.Services.Interfaces;

namespace ShoesShop.MVC
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			builder.Services.Configure<MvcOptions>(options =>
			{
				options.Filters.Add(new RequireHttpsAttribute());
			});

			// Add services to the container.
			builder.Services.AddControllersWithViews();

			#region httpClient

			builder.Services.AddHttpClient<IAccountApiClient, AccountApiClient>(client =>
			{
				client.BaseAddress = new Uri(builder.Configuration["ApiSettings:BaseUrl"]!);
			});

			builder.Services.AddHttpClient<IProductApiClient, ProductApiClient>(client =>
			{
				client.BaseAddress = new Uri(builder.Configuration["ApiSettings:BaseUrl"]!);
			});

			builder.Services.AddHttpClient<IColorApiClient, ColorApiClient>(client =>
			{
				client.BaseAddress = new Uri(builder.Configuration["ApiSettings:BaseUrl"]!);
			});

			builder.Services.AddHttpClient<ITagApiClient, TagApiClient>(client =>
			{
				client.BaseAddress = new Uri(builder.Configuration["ApiSettings:BaseUrl"]!);
			});

			builder.Services.AddHttpClient<ICategoryApiClient, CategoryApiClient>(client =>
			{
				client.BaseAddress = new Uri(builder.Configuration["ApiSettings:BaseUrl"]!);
			});

			builder.Services.AddHttpClient<IProductCategoryApiClient, ProductCategoryApiClient>(client =>
			{
				client.BaseAddress = new Uri(builder.Configuration["ApiSettings:BaseUrl"]!);
			});

			builder.Services.AddHttpClient<IGalleryApiClient, GalleryApiClient>(client =>
			{
				var apiBaseUrl = builder.Configuration["ApiSettings:BaseUrl"];
				if (string.IsNullOrWhiteSpace(apiBaseUrl))
				{
					throw new InvalidOperationException("ApiSettings:BaseUrl Has Not Been Set...");
				}
				client.BaseAddress = new Uri(apiBaseUrl);
				client.Timeout = TimeSpan.FromSeconds(60);
			});

			builder.Services.AddHttpClient<IProductColorApiClient, ProductColorApiClient>(client =>
			{
				client.BaseAddress = new Uri(builder.Configuration["ApiSettings:BaseUrl"]!);
			});

			builder.Services.AddHttpClient<IProductTagApiClient, ProductTagApiClient>(client =>
			{
				client.BaseAddress = new Uri(builder.Configuration["ApiSettings:BaseUrl"]!);
			});

			builder.Services.AddHttpClient<IProductSizeApiClient, ProductSizeApiClient>(client =>
			{
				client.BaseAddress = new Uri(builder.Configuration["ApiSettings:BaseUrl"]!);
			});

			#endregion

			#region Cookie Settings

			builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
			{
				options.LoginPath = "/Account/Login";
				options.AccessDeniedPath = "/Account/AccessDenied";
				options.Cookie.Name = "ShoesShpo.Auth";
				options.Cookie.HttpOnly = true;
				options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
				options.Cookie.SameSite = SameSiteMode.Lax;
				options.ExpireTimeSpan = TimeSpan.FromHours(2);
				options.SlidingExpiration = true;
			});

			#endregion

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (!app.Environment.IsDevelopment())
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			var reWriteOptions = new RewriteOptions().AddRedirectToHttps();
			app.UseRewriter(reWriteOptions);

			app.UseHttpsRedirection();

			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthentication();

			app.UseAuthorization();

			app.MapControllerRoute(
				 name: "areas",
				 pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

			app.MapControllerRoute(
				name: "default",
				pattern: "{controller=Home}/{action=Index}/{id?}");

			app.Run();
		}
	}
}