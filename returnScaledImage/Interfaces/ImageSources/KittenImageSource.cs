using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net.Http;
using System.Threading.Tasks;

namespace returnScaledImage.Interfaces.ImageSources
{
    public class KittenImageSource : IImageSource
    {
        public string Type { get; } = "kitten";

        private readonly IHttpClientFactory _httpClientFactory;

        public KittenImageSource(IHttpClientFactory httpclientfactory)
        {
            _httpClientFactory = httpclientfactory;
        }
        public async Task<List<Image>> GetImages(int initialWidth, int initialHeight)
        {
            var client = _httpClientFactory.CreateClient();
            var stream = await client.GetStreamAsync($"https://placekitten.com/{initialWidth}/{initialHeight}");
            var image = Image.FromStream(stream);

            image = new Bitmap(image);
            Console.WriteLine("Getting image from kitten image source");
            return new List<Image> { image };
        }
    }
}
