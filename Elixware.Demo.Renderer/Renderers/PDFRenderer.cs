using Demo.Common.Models;
using Demo.Renderer.Renderers.ABCPdf;
using Demo.Renderer.Utilities;
using WebSupergoo.ABCpdf13;

namespace Demo.Renderer.Renderers
{
    internal class PDFRenderer : IRenderer
    {
        public async Task<byte[]> RenderDocumentAsync(InputData input)
        {
            await Task.Delay(0);
            using var pdf = new Doc();
            if (input.TemplateFileData != null)
            {
                // Load PDF template
                pdf.Read(input.TemplateFileData);
            }
            ApplyConfig(pdf, input.Config);
            PdfContentRenderer.RenderContent(pdf, input);
            PdfTableRenderer.RenderTables(pdf, input);
            RenderImages(pdf, input);
            pdf.Flatten();
            // TODO: Maybe use MemoryStream
            return pdf.GetData();
        }

        private static void ApplyConfig(Doc pdf, InputConfig? config)
        {
            if (config == null)
            {
                UseDefaultPdfConfig(pdf);
                return;
            }
            pdf.FontSize = config.FontSize;
        }

        private static void UseDefaultPdfConfig(Doc pdf)
        {
            pdf.FontSize = 16;
        }

        private static void RenderImages(Doc pdf, InputData input)
        {
            if(input.Images == null)
            {
                return;
            }
            foreach (var item in input.Images)
            {
                var imageInfo = item.Value;
                var imageBytes = ImageLoader.LoadImageAsBytes(imageInfo);
                var img = new XImage();
                img.SetData(imageBytes);
                pdf.AddImageObject(img, false);
            }
        }
    }
}
