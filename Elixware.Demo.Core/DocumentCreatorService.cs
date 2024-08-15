using Demo.Common.Models;
using Demo.Common.Models.Request;
using Demo.Common.Models.Result;
using Demo.Renderer;
using Demo.Templates;

namespace Demo.Core
{
    internal class DocumentCreatorService : IDocumentCreatorService
    {
        private readonly IRendererProvider _rendererProvider;
        private readonly ITemplateLoader _templateLoader;

        public DocumentCreatorService(IRendererProvider rendererProvider, ITemplateLoader templateProvider)
        {
            _rendererProvider = rendererProvider;
            _templateLoader = templateProvider;
        }

        public async Task<ServiceResult<OutputFileData>> CreateDocumentAsync(InputDataRequest input)
        {
            try
            {
                var rendererInput = new InputData()
                {
                    Config = input.Config,
                    Content = input.Content,
                    Tables = input.Tables,
                    Images = input.Images,
                    OutputFileName = input.OutputFileName
                };
                var renderer = _rendererProvider.GetRenderer(input.Template);
                if (input.Template != null)
                {
                    var templateData = await _templateLoader.LoadTemplateAsync(input.Template);
                    rendererInput.TemplateFileData = templateData;
                }
                var fileData = await renderer.RenderDocumentAsync(rendererInput);
                var outputData = new OutputFileData()
                {
                    FileData = fileData,
                    FileName = input.OutputFileName
                };
                return ServiceResult<OutputFileData>.CreateSuccessResult(outputData);
            }
            catch (Exception ex)
            {
                var result = ServiceResult<OutputFileData>.CreateErrorResult(ex);
                if (!string.IsNullOrWhiteSpace(ex.StackTrace))
                {
                    result.Notes.Add(ex.StackTrace);
                }
                return result;
            }
        }
    }
}
