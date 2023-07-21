using Common.Commons;
using Example_Project.Config;
using Example_Project.Config.Abtracts;
using Example_Project.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Repository.EF;

namespace Example_Project
{
    public class Startup
    {
       
        public Startup(IConfiguration configuration)
        {
            // Load configuration
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public async void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DbContextSql>(options =>
            {
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection"));
            }, ServiceLifetime.Transient);

            services.AddControllers();
            services.AddCustomSwagger();
            services.AddDependency();
            services.AddAutoMapper(typeof(AutoMapperConfig).Assembly);
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.ConfigureCors(Configuration);
            services.AddSwaggerGenNewtonsoftSupport();
            services.AddDistributedMemoryCache();
            services.AddSession();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.ConfigureCors(Configuration);
            services.AddSingleton<ILogger, Logger>();

            services.AddSingleton<IConfigManager, ConfigManager>();

            // Config service provider
            ConfigContainerDJ._serviceProvider = services.BuildServiceProvider();           
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("AnotherPolicy");
            app.UseHttpsRedirection();
            app.UseCustomSwagger();
            app.UseRouting();
            app.UseAuthorization();
            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
