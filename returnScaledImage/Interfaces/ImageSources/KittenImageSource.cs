using LazyCache;
using Microsoft.Extensions.Options;
using returnScaledImage.Models;
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
        private readonly ImageSizeOptions _imageSizeOptions;

        public KittenImageSource(IHttpClientFactory httpclientfactory, IAppCache cache, IOptions<ImageSizeOptions> imageSizeOptions)
        {
            _httpClientFactory = httpclientfactory;
            _cache = cache;
            _imageSizeOptions = imageSizeOptions.Value;
        }
        public async Task<List<Image>> GetImages()
        {
            List<Image> images = new List<Image>();
            Image image;
            foreach (var imageSize in _imageSizeOptions.Sizes)
            {
                var client = _httpClientFactory.CreateClient();
                var stream = await client.GetStreamAsync($"https://placekitten.com/{imageSize.Width}/{imageSize.Height}");
                image = Image.FromStream(stream);
                image = new Bitmap(image);
                images.Add(image);
            }
            Console.WriteLine("Getting image from nature retreiver");
            return images;
        }
    }
}
