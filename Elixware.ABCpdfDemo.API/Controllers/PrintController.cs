using Demo.Common.Models.Request;
using Demo.Core;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace ABCpdfDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PrintController : ControllerBase
    {
        private readonly ILogger<PrintController> _logger;
        private readonly IDocumentCreatorService _documentCreatorService;

        public PrintController(ILogger<PrintController> logger, IDocumentCreatorService documentCreatorService)
        {
            _logger = logger;
            _documentCreatorService = documentCreatorService;
        }

        [HttpPost]
        public async Task<IActionResult> PrintDocumentAsync([FromBody] InputDataRequest request)
        {
            try {
                var result = await _documentCreatorService.CreateDocumentAsync(request);
                if (result.IsError)
                {
                    _logger.LogWarning("{message}", result.Message);
                    return BadRequest(result);
                }
                if (result.IsDataNull || result.Data!.FileData == null)
                {
                    _logger.LogWarning("No data to display");
                    return NoContent();
                }
                return File(result.Data.FileData, MediaTypeNames.Application.Pdf, result.Data.FileName);
            }
            catch(Exception ex)
            {
                _logger.LogError("{error}\n{stacktrace}", ex.Message, ex.StackTrace);
                return StatusCode(500, ex.Message);
            }

        }
    }
}
