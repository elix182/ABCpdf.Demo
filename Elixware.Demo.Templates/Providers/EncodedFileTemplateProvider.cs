using Demo.Common.Models;

namespace Demo.Templates.Providers
{
    internal class EncodedFileTemplateProvider : ITemplateProvider
    {
        public async Task<byte[]?> LoadTemplateAsync(TemplateInfo template)
        {
            await Task.Delay(0);
            string encodedFile = template.EncodedFile;
            if(string.IsNullOrWhiteSpace(encodedFile)){
                throw new ArgumentNullException(nameof(template.EncodedFile));
            }
            return Convert.FromBase64String(encodedFile);
        }
    }
}
