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

        public NatureImageSource(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        

        public async Task<List<Image>> GetImages(int initialWidth, int initialHeight)
        {
            var client = _httpClientFactory.CreateClient();
            var stream = await client.GetStreamAsync($"https://picsum.photos/{initialWidth}/{initialHeight}");
            var image = Image.FromStream(stream);

            image = new Bitmap(image);
            Console.WriteLine("Getting image from nature retreiver");
            return new List<Image> {image};
        }
    }
}
