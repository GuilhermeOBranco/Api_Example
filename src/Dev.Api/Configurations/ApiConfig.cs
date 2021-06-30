using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Dev.Api.Configurations
{
    public static class ApiConfig
    {
        public static IServiceCollection WebApiConfig(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("Developer", builder =>
                {
                    builder.AllowAnyHeader()
                    .AllowAnyOrigin()
                    .AllowAnyMethod();
                });
            });

            services.Configure<ApiBehaviorOptions>(options =>
           {
               options.SuppressModelStateInvalidFilter = true;
           });
            return services;
        }
        public static IApplicationBuilder UseMvcConfigurantion(this IApplicationBuilder app)
        {
            
            app.UseCors("Developer");
            return app;
        }

    }
}