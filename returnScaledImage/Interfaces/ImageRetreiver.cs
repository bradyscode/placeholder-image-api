using returnScaledImages.Controllers;
using System;
using System.Drawing;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace returnScaledImage.Interfaces
{
    public class ImageRetreiver : IImageRetreiver
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ImageRetreiver(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<Image> GetImageAsync(int width, int height, int initialWidth, int initialHeight)
        {
            var client = _httpClientFactory.CreateClient();
            var stream = await client.GetStreamAsync($"https://picsum.photos/{initialWidth}/{initialHeight}");
            var image = Image.FromStream(stream);

            image = new Bitmap(image, new Size(width, height));
            Console.WriteLine("Getting image from image retreiver");
            return image;
        }
    }
}
