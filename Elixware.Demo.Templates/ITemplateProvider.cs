namespace Demo.Templates
{
    public interface ITemplateProvider
    {
        public Task<byte[]?> LoadTemplateAsync(string template);
    }
}
