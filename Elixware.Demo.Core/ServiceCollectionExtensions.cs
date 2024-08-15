using Demo.Renderer;
using Demo.Templates;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Demo.Core
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDocumentCreatorServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddRendererServices(configuration);
            services.AddTemplateProvidersServices(configuration);
            services.AddScoped<IDocumentCreatorService, DocumentCreatorService>();
            return services;
        }
    }
}
