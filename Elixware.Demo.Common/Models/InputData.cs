namespace Demo.Common.Models
{
    public class InputData
    {
        public string OutputFileName { get; set; } = string.Empty;
        public byte[]? TemplateFileData { get; set; } = null;
        public InputConfig? Config { get; set; } = null;
        public IDictionary<string, object>? Content { get; set; } = new Dictionary<string, object>();
        public IDictionary<string, TableInfo>? Tables { get; set; } = new Dictionary<string, TableInfo>();
        public IDictionary<string, ImageInfo>? Images { get; set; } = new Dictionary<string, ImageInfo>();
    }
}
