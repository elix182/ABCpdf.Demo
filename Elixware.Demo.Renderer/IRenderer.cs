using Demo.Common.Models;

namespace Demo.Renderer
{
    public interface IRenderer
    {
        public Task<byte[]> RenderDocumentAsync(InputData input);
    }
}
