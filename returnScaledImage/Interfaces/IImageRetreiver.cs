using System.Drawing;
using System.Threading.Tasks;

namespace returnScaledImage.Interfaces
{
    public interface IImageRetreiver
    {
        Task<Image> GetImageAsync(int width, int height, int initialWidth, int initialHeight);
    }
}
