using Example_Project.Services.Implement;
using Microsoft.Extensions.DependencyInjection;
using Repository.Repositories;

namespace Example_Project.Extensions
{
    public static class DependencyInjectionExtension
    {
        public static void AddDependency(this IServiceCollection services)
        {
            //Repository
            services.AddTransient(typeof(IBaseRepositorySql<>), typeof(BaseRepositorySql<>));
            services.AddTransient<IUserRepository, UserRepository>();

            //Inject Service
            services.AddTransient<IUserService, UserService>();
        }
    }
}
