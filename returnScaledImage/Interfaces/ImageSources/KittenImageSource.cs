using LazyCache;
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
        private readonly IAppCache _cache;

        public KittenImageSource(IHttpClientFactory httpclientfactory, IAppCache cache)
        {
            _httpClientFactory = httpclientfactory;
            _cache = cache;
        }
        public async Task<List<Image>> GetImages(int initialWidth, int initialHeight)
        {
            List<Image> images = new List<Image>();
            Image image;
            for (int i = 1; i < 10; i++)
            {
                var client = _httpClientFactory.CreateClient();
                var stream = await client.GetStreamAsync($"https://placekitten.com/{initialWidth}/{initialHeight}");
                image = Image.FromStream(stream);
                client.Dispose();
                image = new Bitmap(image);

                images.Add(image);
                _cache.Add($"{initialWidth},{initialHeight}", image);

                initialHeight += 100;
                initialWidth += 100;
            }
            Console.WriteLine("Getting image from kitten retreiver");
            Console.WriteLine(images.Count);
            return images;
        }
    }
}
