using Demo.Common.Models;

namespace Demo.Templates
{
    internal interface ITemplateProvider
    {
        public Task<byte[]?> LoadTemplateAsync(TemplateInfo template);
    }
}
