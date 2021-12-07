using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;

namespace returnScaledImage.Interfaces
{
    public interface IImageSource
    {
        public string Type { get; }
        Task<List<Image>> GetImages(int initialWidth, int initialHeight);
    }
}
