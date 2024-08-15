using Demo.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ABCpdfDemo.ConsoleApp
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDemoServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDocumentCreatorServices(configuration);
            return services;
        }
    }
}
