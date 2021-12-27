using System.Collections.Generic;

namespace returnScaledImage.Models
{
    public class Size
    {
        public int Width { get; set; }
        public int Height { get; set; }
    }

    public class ImageSizeOptions
    {
        public List<Size> Sizes { get; set; }
    }
}
