using Demo.Common.Models;

namespace Demo.Renderer
{
    public interface IRendererProvider
    {
        public IRenderer GetRenderer(TemplateInfo? template);
    }
}
