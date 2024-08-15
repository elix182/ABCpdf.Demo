using Demo.Core;

namespace ABCpdfDemo
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
