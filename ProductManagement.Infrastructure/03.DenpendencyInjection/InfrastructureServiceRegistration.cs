using Catalog.Domain._01.Entities;
using Catalog.Infrastructure._02.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductManagement.Infrastructure;
using ProductManagement.Infrastructure.Data;
using ProductManagement.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Infrastructure._03.DenpendencyInjection
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
           

            services.Configure<JwtSettings>(options =>
            {
                options.SecretKey = configuration["JwtSettings:SecretKey"];
                options.Issuer = configuration["JwtSettings:Issuer"];
                options.Audience = configuration["JwtSettings:Audience"];
                var expiryMinutes = configuration["JwtSettings:ExpiryMinutes"];
                options.ExpiryMinutes = string.IsNullOrEmpty(expiryMinutes) ? 60 : int.Parse(expiryMinutes);
            });

           

            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IDatabaseContext, DapperContext>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();           

            return services;
        }
    }
}
