using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Demo.Templates
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddTemplateProvidersServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ITemplateProvider, FileTemplateProvider>();
            return services;
        }
    }
}
