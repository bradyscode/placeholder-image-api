using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;

namespace returnScaledImage.Interfaces
{
    public interface IImageSource
    {
        Task<List<Image>> GetImages();
    }
}
