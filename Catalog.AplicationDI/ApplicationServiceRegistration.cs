using Catalog.Aplication.Mapping;
using Catalog.QueriesService;
using Microsoft.Extensions.DependencyInjection;
using ProductManagement.catalog.Queries;

namespace Catalog.AplicationDI
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Registrar AutoMapper con los perfiles de esta capa
            //services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddAutoMapper(typeof(ProductoProfile).Assembly);

            // Registrar servicios de aplicación
            services.AddScoped<IProductQueryService, ProductQueryService>();            
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ITokenJWT, TokenJWT>();   
            return services;
        }
    }
}