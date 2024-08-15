using Demo.Common.Models;
using Demo.Renderer.Constants;
using Demo.Renderer.Utilities;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using WebSupergoo.ABCpdf13;

namespace Demo.Renderer.Renderers
{
    // Docs: https://www.websupergoo.com/helppdfnet/default.htm?page=source%2f5-abcpdf%2fxreadoptions%2f2-properties%2f1-readmodule.htm
    internal class XmlDocRenderer : IRenderer
    {
        public async Task<byte[]> RenderDocumentAsync(InputData input)
        {
            await Task.Delay(0);
            using var doc = OpenDocForEditing(input, out var docStream);
            PopulateDocumentContent(doc, input);
            doc.Save();
            using var m = new MemoryStream();
            docStream.CopyTo(m);
            File.WriteAllBytes("test.docx", m.ToArray());
            // Convert Docx to PDF
            using var pdf = new Doc();
            var readOptions = new XReadOptions
            {
                FileExtension = FileExtensions.Docx,
                ReadModule = ReadModuleType.Default
            };
            pdf.Read(docStream, readOptions);
            pdf.SaveOptions.FileExtension = FileExtensions.PDF;
            docStream.Close();
            return pdf.GetData();
        }

        private WordprocessingDocument OpenDocForEditing(InputData input, out Stream stream)
        {
            var docStream = new MemoryStream();
            stream = docStream;
            WordprocessingDocument doc;
            if(input.TemplateFileData != null)
            {
                docStream.Write(input.TemplateFileData);
                doc = WordprocessingDocument.Open(stream, true);
            }
            else
            {
                doc = WordprocessingDocument.Create(stream, WordprocessingDocumentType.Document);
            }
            return doc;
        }

        private void PopulateDocumentContent(WordprocessingDocument doc, InputData input)
        {
            MainDocumentPart? mainPart = doc.MainDocumentPart;
            if(mainPart == null)
            {
                // what
                return;
            }
            var contentControls = mainPart.Document.Descendants<SdtElement>();
            // Replace Text content controls
            if (input.Content != null)
            {
                foreach (var control in contentControls)
                {
                    var tag = control.SdtProperties?.GetFirstChild<Tag>();
                    string? tagValue = tag?.Val;
                    if (tagValue == null)
                    {
                        continue;
                    }
                    var textControl = control.Descendants<Text>().FirstOrDefault();
                    control.Descendants<Text>().Skip(1).ToList().ForEach(t => t.Remove());
                    if (textControl == null)
                    {
                        continue;
                    }
                    var value = input.Content[tagValue];
                    var inputValue = value?.ToString() ?? string.Empty;
                    textControl.Text = inputValue;
                }
            }
            // Replace Image content controls
            if (input.Images != null)
            {
                foreach (var control in contentControls)
                {
                    var tag = control.SdtProperties?.GetFirstChild<Tag>();
                    string? tagValue = tag?.Val;
                    if (tagValue == null || !input.Images.TryGetValue(tagValue, out ImageInfo? value))
                    {
                        continue;
                    }
                    var imageControl = control.Descendants<Drawing>().FirstOrDefault();
                    control.Descendants<Drawing>().Skip(1).ToList().ForEach(t => t.Remove());
                    if (imageControl == null)
                    {
                        continue;
                    }
                    var blip = imageControl.Descendants<DocumentFormat.OpenXml.Drawing.Blip>().FirstOrDefault();
                    if(blip == null || blip.Embed == null)
                    {
                        continue;
                    }
                    var idpp = mainPart.Parts.Where(p => p.RelationshipId == blip.Embed).FirstOrDefault();
                    var ip = idpp.OpenXmlPart as ImagePart;
                    if(ip == null)
                    {
                        continue;
                    }
                    // load image
                    using var imageStream = ImageLoader.LoadImageAsStream(value);
                    if (imageStream == null)
                    {
                        continue;
                    }
                    ip.FeedData(imageStream);
                    imageStream.Dispose();
                }
            }
        }
    }
}
