using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using System.Net;
using System.Drawing.Imaging;
using LazyCache;
using System.Net.Http;
using returnScaledImage.Interfaces;

namespace returnScaledImages.Controllers
{
    
    public class returnImageController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IAppCache _cache;
        private readonly IImageRetreiver _imageRetriever;

        public returnImageController(IHttpClientFactory httpClientFactory, IAppCache cache, IImageRetreiver imageRetriever)
        {
            _httpClientFactory = httpClientFactory;
            _cache = cache;
            _imageRetriever = imageRetriever;
        }


        [HttpPost("/{source}/{width}/{height}")]
        public ActionResult ReturnScaledImage(int width, int height)
        {
            string url = "https://picsum.photos/2000/2000";
            var webClient = new WebClient();
            byte [] data = webClient.DownloadData(url);
            MemoryStream memoryStream = new MemoryStream(data); //These lines are needed for URL input

            //var image = Image.FromFile(filePath); //for picture in files

            var image = Image.FromStream(memoryStream);

            if (2000 % width == 0 && 2000 % height ==0)
            {
                image = image.resizeImage(new Size(width, height));

                byte[] bytes = (byte[])(new ImageConverter()).ConvertTo(image, typeof(byte[]));

                return File(bytes, "image/jpeg", "resizedImage.jpg");
            }
            else
            {
                return BadRequest("Image must be perfect squares! i.e. The width and height divided by the original size must have a remainder of 0");
            }
         
        }

        //Adding an endpoint that does not care about aspect ratio
        [HttpPost("/{source}/noaspectratio")]
        public async Task<ActionResult> ReturnScaledImageNoAspectRatio(int width, int height, int initialWidth, int initialHeight, string source)
        {
            try
            {
                var image = await _imageRetriever.GetImageAsync(width, height, initialWidth, initialHeight, source.ToLower());

                return File(image.GetBytes(), "image/jpeg", "resizedImageNAR");
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest("Invalid image source");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Cannot connect to image source. Please try another image source.");
            }

        }



    }
    public static class imageExtensions
    {
        public static Image resizeImage(this Image img, Size size)
        {
            int sourceWidth = img.Width;
            int sourceHeight = img.Height;
            float nPercent = 0;
            float nPercentW = 0;
            float nPercentH = 0;

            //calculating width with desired float
            nPercentW = ((float)size.Width / (float)sourceWidth);
            //calculating height with desired float
            nPercentH = ((float)size.Height / (float)sourceHeight);
            if (nPercentH < nPercentW)
                nPercentW = nPercentH;
            else
                nPercent = nPercentW;
            int destWidth = (int)(sourceWidth * nPercent);
            int destHeight = (int)(sourceHeight * nPercent);

            Bitmap b = new Bitmap(destWidth, destHeight);
            Graphics g = Graphics.FromImage((System.Drawing.Image)b);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

            g.DrawImage(img, 0, 0, destWidth, destHeight);

            return (System.Drawing.Image)b;
        }

        public static byte[] GetBytes(this Image image)
        {
            var imageStream = new MemoryStream();
            image.Save(imageStream, ImageFormat.Jpeg);

            return imageStream.ToArray();
        }
    }
}
