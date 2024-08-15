using Demo.Common.Models;

namespace Demo.Templates
{
    public interface ITemplateLoader
    {
        public Task<byte[]?> LoadTemplateAsync(TemplateInfo? template);
    }
}
