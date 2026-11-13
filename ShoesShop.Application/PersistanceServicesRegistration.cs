using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using ShoesShop.Application.Common.Behaviors;

namespace ShoesShop.Application
{
    public static class PersistanceServicesRegistration
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(
                typeof(PersistanceServicesRegistration).Assembly
            );

            services.AddValidatorsFromAssembly(typeof(PersistanceServicesRegistration).Assembly);

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            return services;
        }
    }
}
