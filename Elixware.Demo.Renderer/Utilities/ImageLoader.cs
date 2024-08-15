using Demo.Common.Models;
using System.Text;

namespace Demo.Renderer.Utilities
{
    internal class ImageLoader
    {
        public static Stream? LoadImageAsStream(ImageInfo imageInfo)
        {
            if(imageInfo == null)
            {
                return null;
            }
            Stream? stream = null;
            if (!string.IsNullOrWhiteSpace(imageInfo.Path))
            {
                stream = File.OpenRead(imageInfo.Path);
            }
            else if (!string.IsNullOrWhiteSpace(imageInfo.EncodedImage))
            {
                var imageBytes = Convert.FromBase64String(imageInfo.EncodedImage);
                stream = new MemoryStream(imageBytes);
            }
            else if (!string.IsNullOrWhiteSpace(imageInfo.Url))
            {
                using var client = new HttpClient();
                stream = client.GetStreamAsync(imageInfo.Url).GetAwaiter().GetResult();
            }
            return stream;
        }

        public static byte[]? LoadImageAsBytes(ImageInfo imageInfo)
        {
            using var imageStream = LoadImageAsStream(imageInfo);
            if(imageStream == null)
            {
                return null;
            }
            using var memoryStream = new MemoryStream();
            imageStream.CopyTo(memoryStream);
            return memoryStream.ToArray();
        }

        public static string LoadImageForHtml(ImageInfo imageInfo)
        {
            if (imageInfo == null)
            {
                return string.Empty;
            }
            string imageSrc = string.Empty;
            if (!string.IsNullOrWhiteSpace(imageInfo.Url))
            {
                imageSrc = imageInfo.Url;
            }
            else if (!string.IsNullOrWhiteSpace(imageInfo.EncodedImage))
            {
                var mime = ImageUtility.GetMediaTypeFromFile(imageInfo.FileName);
                imageSrc = $"data:{mime};base64,{imageInfo.EncodedImage}";
            }
            else if (!string.IsNullOrWhiteSpace(imageInfo.Path))
            {
                var image = LoadImageAsBytes(imageInfo);
                if (image == null)
                {
                    return string.Empty;
                }
                var mime = ImageUtility.GetMediaTypeFromFile(imageInfo.Path);
                var imageEncoded = Convert.ToBase64String(image);
                imageSrc = $"data:{mime};base64,{imageEncoded}";
            }
            return imageSrc;
        }
    }
}
