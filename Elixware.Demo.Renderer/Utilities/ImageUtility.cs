using Demo.Renderer.Constants;
using System.Net.Mime;

namespace Demo.Renderer.Utilities
{
    public static class ImageUtility
    {
        public static string GetMediaTypeFromFile(string? filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                return MediaTypeNames.Image.Jpeg;
            }
            string fileExtension = Path.GetExtension(filePath);
            switch (fileExtension.ToLower())
            {
                case FileExtensions.TIFF:
                    return MediaTypeNames.Image.Tiff;
                case FileExtensions.PNG:
                    return MediaTypeNames.Image.Png;
                //case FileExtensions.JPG:
                //case FileExtensions.JPEG:
                default:
                    return MediaTypeNames.Image.Jpeg;
            }
        }
    }
}
