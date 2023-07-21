using Common;
using Example_Project.Config;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;

namespace Example_Project.Extensions
{
    public static class SwaggerExtension
    {
        private static string stateSource = ConfigManager.StaticGet(Constants.CONF_STATE_SOURCE);
        public static IServiceCollection AddCustomSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Swagger Chatbot API", Version = "v1" });
                c.OperationFilter<AuthorizationOperationFiltercs>();
            });
            return services;
        }

        public static IApplicationBuilder UseCustomSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger().UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Recording API");
                options.DocumentTitle = "Recording API";
                options.RoutePrefix = String.Empty;  // Set Swagger UI at apps root
            });
            return app;
        }
    }
}
