using Demo.Common.Models;
using Demo.Renderer.Constants;
using WebSupergoo.ABCpdf13;

namespace Demo.Renderer.Renderers
{
    internal class MSDocRenderer : IRenderer
    {
        public async Task<byte[]> RenderDocumentAsync(InputData input)
        {
            string tempFilePath = Path.GetTempFileName();
            if(input.TemplateFileData != null)
            {
                await File.WriteAllBytesAsync(tempFilePath, input.TemplateFileData);
            }
            else
            {
                _ = File.Create(tempFilePath);
            }
            PopulateDocumentContent(tempFilePath, input);
            using var pdf = new Doc();
            var readOptions = new XReadOptions
            {
                FileExtension = FileExtensions.Doc,
                ReadModule = ReadModuleType.Default
            };
            pdf.Read(new MemoryStream(File.ReadAllBytes(tempFilePath)), readOptions);
            pdf.SaveOptions.FileExtension = FileExtensions.PDF;
            File.Delete(tempFilePath);
            return pdf.GetData();
        }

        private void PopulateDocumentContent(string tempFile, InputData input)
        {
            //var word = new Microsoft.Office.Interop.Word.Application();
            //var doc = word.Documents.Open(tempFilePath);
            //if (input.Content != null)
            //{
            //    foreach (Microsoft.Office.Interop.Word.Bookmark bookmark in doc.Bookmarks)
            //    {
            //        if (bookmark.Name == null || !input.Content.TryGetValue(bookmark.Name, out object? value))
            //        {
            //            continue;
            //        }
            //        bookmark.Range.Text = value.ToString();
            //    }
            //}
            //doc.Save();
            // Convert Doc to PDF
        }
    }
}
