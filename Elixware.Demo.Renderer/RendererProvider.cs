using Demo.Common.Models;
using Demo.Renderer.Renderers;

namespace Demo.Renderer
{
    internal class RendererProvider : IRendererProvider
    {
        public IRenderer GetRenderer(TemplateInfo? template)
        {
            // If no template is defined, use PDF renderer
            if(template == null)
            {
                return new PDFRenderer();
            }
            return template.TemplateType switch
            {
                TemplateType.Pdf => new PDFRenderer(),
                TemplateType.Html => new HtmlRenderer(),
                TemplateType.Docx => new XmlDocRenderer(),
                TemplateType.Doc => new MSDocRenderer(),
                _ => throw new NotSupportedException(nameof(template.TemplateType)),
            };
        }
    }
}
