using returnScaledImages.Controllers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace returnScaledImage.Interfaces
{
    public class ImageRetreiver : IImageRetreiver
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IEnumerable<IImageSource> _imageSources;

        public ImageRetreiver(IHttpClientFactory httpClientFactory, IEnumerable<IImageSource> imageSource)
        {
            _httpClientFactory = httpClientFactory;
            _imageSources = imageSource;
        }

        public async Task<Image> GetImageAsync(int width, int height, int initialWidth, int initialHeight, string source)
        {
            Random rnd = new Random();
            IImageSource imageSource = _imageSources.Single(x => x.Type.Equals(source.ToLower()));
            var images = await imageSource.GetImages(initialWidth, initialHeight);
            var image = images[3];
            image = new Bitmap(image, new Size(width, height));
            return image;
        }
    }
}
