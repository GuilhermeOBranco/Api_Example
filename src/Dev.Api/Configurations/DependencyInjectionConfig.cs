using Dev.Data.Context;
using DevIO.Business.Intefaces;
using DevIO.Data.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace Dev.Api.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<ApiContext>();
            services.AddScoped<IFornecedorRepository, FornecedorRepository>();

            return services;
        }
    }
}