using Microsoft.OpenApi.Models;
using ShoesShop.Api.Extensions;
using ShoesShop.Api.Middlewares;
using ShoesShop.Application;
using ShoesShop.Identity;
using ShoesShop.Persistance;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
AddSwagger(builder.Services);
builder.Services.AddApplication();
builder.Services.ConfigurePersistanceServices(builder.Configuration);
builder.Services.ConfigureIdentityServices(builder.Configuration);

var app = builder.Build();

app.UseGlobalExceptionHandling();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseMiddleware<ValidationExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();

void AddSwagger(IServiceCollection services)
{
	services.AddSwaggerGen(o =>
	{
		o.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
		{
			Description = "This Is Shoes Shop Swagger...",
			Name = "Authorization",
			In = ParameterLocation.Header,
			Type = SecuritySchemeType.ApiKey,
			Scheme = "Bearer"
		});

		o.AddSecurityRequirement(new OpenApiSecurityRequirement()
		{
			{
			new OpenApiSecurityScheme()
			{
				Reference = new OpenApiReference()
				{
					Type = ReferenceType.SecurityScheme ,
					Id = "Bearer"
				},
				Scheme = "oauth2",
				Name = "Bearer",
				In = ParameterLocation.Header
			},
			new List<string>()
			}
		});

		o.SwaggerDoc("v1", new OpenApiInfo()
		{
			Version = "v1",
			Title = "Shoes Shop Api"
		});
	});
}