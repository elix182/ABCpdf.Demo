using Demo.Common.Models;
using Demo.Common.Models.Request;
using Demo.Common.Models.Result;

namespace Demo.Core
{
    public interface IDocumentCreatorService
    {
        public Task<ServiceResult<OutputFileData>> CreateDocumentAsync(InputDataRequest input);
    }
}
