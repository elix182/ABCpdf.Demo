namespace Demo.Common.Models
{
    public class TemplateInfo
    {
        public string Path { get; set; } = string.Empty;
        public string EncodedFile { get; set; } = string.Empty;
        public TemplateType TemplateType { get; set; } = TemplateType.None;
    }
}
