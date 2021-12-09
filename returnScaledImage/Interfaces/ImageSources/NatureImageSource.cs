using LazyCache;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net.Http;
using System.Threading.Tasks;

namespace returnScaledImage.Interfaces.ImageSources
{
    public class NatureImageSource : IImageSource
    {
        public string Type => "nature";
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IAppCache _cache;

        public NatureImageSource(IHttpClientFactory httpClientFactory, IAppCache cache)
        {
            _httpClientFactory = httpClientFactory;
            _cache = cache;
        }

        

        public async Task<List<Image>> GetImages(int initialWidth, int initialHeight)
        {
            List<Image> images = new List<Image>();
            Image image;
            for(int i = 1; i < 10; i++)
            {
                var client = _httpClientFactory.CreateClient();
                var stream = await client.GetStreamAsync($"https://picsum.photos/{initialWidth}/{initialHeight}");
                image = Image.FromStream(stream);
                client.Dispose();
                image = new Bitmap(image);

                images.Add(image);
                _cache.Add($"{initialWidth},{initialHeight}", image);
            }
            Console.WriteLine("Getting image from nature retreiver");
            return images;
        }
    }
}
