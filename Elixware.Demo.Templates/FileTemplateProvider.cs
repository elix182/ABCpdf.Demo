namespace Demo.Templates
{
    internal class FileTemplateProvider : ITemplateProvider
    {
        public async Task<byte[]?> LoadTemplateAsync(string template)
        {
            if (!File.Exists(template))
            {
                throw new FileNotFoundException(template);
            }
            return await File.ReadAllBytesAsync(template);
        }
    }
}
