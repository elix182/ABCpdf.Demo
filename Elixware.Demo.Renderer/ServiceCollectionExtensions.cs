using Demo.Renderer.Renderers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebSupergoo.ABCpdf13;

namespace Demo.Renderer
{
    public static class ServiceCollectionExtensions
    {
        private const string ConfigurationLicenseKey = "ABCPdf_License";
        public static IServiceCollection AddRendererServices(this IServiceCollection services, IConfiguration configuration)
        {
            string licenseKey = configuration[ConfigurationLicenseKey] ?? string.Empty;
            if (!string.IsNullOrWhiteSpace(licenseKey))
            {
                XSettings.InstallLicense(licenseKey);
            }
            services.AddScoped<IRendererProvider, RendererProvider>();
            return services;
        }
    }
}
