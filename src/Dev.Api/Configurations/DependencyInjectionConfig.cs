using Dev.Data.Context;
using DevIO.Business.Intefaces;
using DevIO.Business.Notificacoes;
using DevIO.Business.Services;
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
            services.AddScoped<IFornecedorService, FornecedorService>();
            services.AddScoped<IEnderecoRepository, EnderecoRepository>();
            services.AddScoped<INotificador, Notificador>();
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            services.AddScoped<IProdutoService, ProdutoService>();

            return services;
        }
    }
}