using Microsoft.Extensions.DependencyInjection;
using System;

namespace Example_Project.Config
{
    public class ConfigContainerDJ
    {
        public static IServiceProvider _serviceProvider { get; set; }

        public static T CreateInstance<T>()
        {
            return _serviceProvider.GetService<T>();
        }
    }

}
