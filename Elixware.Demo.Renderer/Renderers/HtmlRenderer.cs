using Demo.Common.Models;
using Demo.Renderer.Utilities;
using Stubble.Core.Builders;
using System.Text;
using WebSupergoo.ABCpdf13;

namespace Demo.Renderer.Renderers
{
    internal class HtmlRenderer : IRenderer
    {
        public async Task<byte[]> RenderDocumentAsync(InputData input)
        {
            string templateHtml = string.Empty;
            if (input.TemplateFileData != null)
            {
                templateHtml = Encoding.UTF8.GetString(input.TemplateFileData);
            }
            var view = CreateRendererView(input);
            var stubble = new StubbleBuilder().Build();
            var html = await stubble.RenderAsync(templateHtml, view);
            using var pdf = new Doc();
            pdf.HtmlOptions.Engine = EngineType.Chrome123;
            pdf.HtmlOptions.UseScript = true; // Enable JavaScript
            pdf.HtmlOptions.Media = MediaType.Print; // Or Screen for a more screen oriented output
            pdf.HtmlOptions.InitialWidth = 800;
            int contentId = pdf.AddImageHtml(html);
            while(true)
            {
                pdf.FrameRect(); // add a black border
                if (!pdf.Chainable(contentId))
                {
                    break;
                }
                pdf.Page = pdf.AddPage();
                contentId = pdf.AddImageToChain(contentId);
            }
            for (int i = 1; i <= pdf.PageCount; ++i)
            {
                pdf.PageNumber = i;
                pdf.Flatten();
            }
            return pdf.GetData();
        }

        private static IDictionary<string, object> CreateRendererView(InputData input)
        {
            var result = new Dictionary<string, object>();
            if (input.Content != null)
            {
                foreach (var item in input.Content)
                {
                    result.Add(item.Key, item.Value);
                }
            }
            if (input.Tables != null)
            {
                const string TablePrefix = "Table";
                foreach (var item in input.Tables)
                {
                    string key = string.Format("{0}__{1}", TablePrefix, item.Key);
                    result.Add(key, item.Value);
                }
            }
            if(input.Images != null)
            {
                const string ImagePrefix = "Image";
                foreach (var item in input.Images)
                {
                    string key = string.Format("{0}__{1}", ImagePrefix, item.Key);
                    var imageInfo = item.Value;
                    string imageSrc = ImageLoader.LoadImageForHtml(imageInfo);
                    result.Add(key, imageSrc);
                }
            }
            return result;
        }
    }
}
