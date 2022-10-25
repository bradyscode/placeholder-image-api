using System.Drawing;
using System.Threading.Tasks;

namespace returnScaledImage.Interfaces.Icon
{
    public interface IIconRetreiver
    {
        Task<Image> GetIconAsync(string icon, string color);
        Task<Image> GetIconAsync(string icon);

    }
}
