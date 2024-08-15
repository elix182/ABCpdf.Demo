using Demo.Common.Models;
using Demo.Templates.Providers;

namespace Demo.Templates
{
    internal sealed class TemplateLoader : ITemplateLoader
    {
        public async Task<byte[]?> LoadTemplateAsync(TemplateInfo? template)
        {
            if(template == null)
            {
                return null;
            }
            ITemplateProvider? provider = null;
            if(!string.IsNullOrWhiteSpace(template.EncodedFile))
            {
                provider = new EncodedFileTemplateProvider();
            }
            else if(!string.IsNullOrWhiteSpace(template.Path))
            {
                provider = new FileTemplateProvider();
            }

            if(provider == null){
                return null;
            }
            return await provider.LoadTemplateAsync(template);
        }
    }
}
