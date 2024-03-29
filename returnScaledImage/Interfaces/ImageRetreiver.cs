﻿using LazyCache;
using Microsoft.Extensions.Options;
using returnScaledImage.Models;
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
        private readonly ImageSizeOptions _imageSizeOptions;
        private readonly IAppCache _cache;

        public ImageRetreiver(IHttpClientFactory httpClientFactory, IEnumerable<IImageSource> imageSource, IOptions<ImageSizeOptions> imageSizeOptions, IAppCache cache)
        {
            _httpClientFactory = httpClientFactory;
            _imageSources = imageSource;
            _imageSizeOptions = imageSizeOptions.Value;
            _cache = cache;

        }

        public async Task<Image> GetImageAsync(int width, int height, int initialWidth, int initialHeight, string source)
        {
            List<decimal> aspectRatios = new List<decimal>();
            Random rnd = new Random();
            IImageSource imageSource = _imageSources.Single(x => x.Type.Equals(source));
            var images = await _cache.GetOrAdd($"{source}", () => imageSource.GetImages());

            if (initialWidth > 0 && initialWidth > 0)
            {
                foreach (var imageSize in images)
                {
                    aspectRatios.Add(GetAspectRatio(imageSize.Width, imageSize.Height));
                }

            }

            decimal closest = 999;
            //Working on algorithm to determine closest aspect ratio
            for (int i = 0; i < aspectRatios.Count; i++)
            {
                var targetRatio = GetAspectRatio(width, height);
                var aRatio = aspectRatios[i];

                if ((targetRatio - aRatio) < closest)
                {
                    closest = aRatio;
                }

            }


            var image = images[rnd.Next(0,images.Count)];
            if (width < 1 || height < 1)
            {
                image = new Bitmap(image);
            }
            else
            {
                image = new Bitmap(image, new System.Drawing.Size(width, height));
            }
            return image;
        }

        private static decimal GetAspectRatio(int width, int height)
        {
            return height / width;
        }
    }
}
