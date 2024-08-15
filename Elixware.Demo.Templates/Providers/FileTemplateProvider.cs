using Demo.Common.Models;

namespace Demo.Templates.Providers
{
    internal class FileTemplateProvider : ITemplateProvider
    {
        public async Task<byte[]?> LoadTemplateAsync(TemplateInfo template)
        {
            string templatePath = template.Path;
            if (!File.Exists(templatePath))
            {
                throw new FileNotFoundException(templatePath);
            }
            return await File.ReadAllBytesAsync(templatePath);
        }
    }
}
