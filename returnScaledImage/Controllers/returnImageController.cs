using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;


namespace returnScaledImages.Controllers
{
    
    public class returnImageController : Controller
    {
        
        [HttpPost("/images/{width}/{height}")]
        public ActionResult ReturnScaledImage(int width, int height)
        {
            var filePath = "k2a53gzxwpz71.jpg";
            var image = Image.FromFile(filePath);
            
            image = image.resizeImage(new Size(width, height));

            byte[] bytes = (byte[])(new ImageConverter()).ConvertTo(image, typeof(byte[]));

            return File(bytes, "image/jpeg", "resizedImage.jpg");            
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
