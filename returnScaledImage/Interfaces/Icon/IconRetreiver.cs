using Serilog;
using System;
using System.Drawing;
using System.Net.Http;
using System.Threading.Tasks;

namespace returnScaledImage.Interfaces.Icon
{
    public class IconRetreiver : IIconRetreiver
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public IconRetreiver(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<Image> GetIconAsync(string icon, string color)
        {
            try
            {
                Image image;
                var client = _httpClientFactory.CreateClient();
                var stream = await client.GetStreamAsync($"https://img.icons8.com/{color}/{icon}");
                image = Image.FromStream(stream);
                image = new Bitmap(image);

                Log.Information("Getting colored icon from IconRetreiver");

                return image;
            }catch (Exception ex)
            {
                Log.Error(ex.Message.ToString());
                throw;
            }

        }

        public async Task<Image> GetIconAsync(string icon)
        {
            try
            {
                Image image;
                var client = _httpClientFactory.CreateClient();
                var stream = await client.GetStreamAsync($"https://img.icons8.com/{icon}");
                image = Image.FromStream(stream);
                image = new Bitmap(image);

                Log.Information("Getting icon from IconRetreiver");

                return image;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message.ToString());
                return null;
            }
        }
    }
}


/**
          string url = $"https://img.icons8.com/{icon}";
            var webClient = new WebClient();
            byte[] data = webClient.DownloadData(url);
            MemoryStream memoryStream = new MemoryStream(data);

            var image = Image.FromStream(memoryStream);


            byte[] bytes = (byte[])(new ImageConverter()).ConvertTo(image, typeof(byte[]));

            return File(bytes, "image/jpeg", "resizedImage.jpg");
**/
