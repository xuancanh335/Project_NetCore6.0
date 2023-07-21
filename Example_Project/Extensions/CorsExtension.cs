using Common;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Example_Project.Extensions
{
    public static class CorsExtension
    {
        public static void ConfigureCors(this IServiceCollection services, IConfiguration config)
        {
            var configValue = config.GetSection(Constants.CONF_CROSS_ORIGIN).Value;
            string[] CORSComplianceDomains = configValue.Split(",");

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:3000");
                    });

                options.AddPolicy("AnotherPolicy",
                    builder =>
                    {
                        builder.WithOrigins(CORSComplianceDomains)
                                            .AllowAnyHeader()
                                            .AllowAnyMethod();
                    });

            });
        }
    }
}
