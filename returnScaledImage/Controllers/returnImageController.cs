using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using System.Net;

namespace returnScaledImages.Controllers
{
    
    public class returnImageController : Controller
    {
        
        [HttpPost("/images/{width}/{height}")]
        public ActionResult ReturnScaledImage(int width, int height)
        {
            string url = "https://picsum.photos/2000/2000";
            var webClient = new WebClient();
            byte [] data = webClient.DownloadData(url);
            MemoryStream memoryStream = new MemoryStream(data); //These lines are needed for URL input

            var filePath = url;

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
    }
}
