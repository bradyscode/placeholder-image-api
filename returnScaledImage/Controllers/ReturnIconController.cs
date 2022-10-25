using Microsoft.AspNetCore.Mvc;
using returnScaledImage.Interfaces.Icon;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace returnScaledImage.Controllers
{
    public class ReturnIconController : Controller
    {
        private readonly IIconRetreiver _iconRetreiver;

        public ReturnIconController(IIconRetreiver iconRetreiver)
        {
            _iconRetreiver = iconRetreiver;
        }
        [HttpGet("/icon")]
        public async Task<ActionResult> GetIcon(string icon)
        {
            var iconResult = await _iconRetreiver.GetIconAsync(icon);
            return Ok(iconResult);
        }
        [HttpGet("/iconColor")]
        public async Task<ActionResult> GetIcon(string icon, string color)
        {
            var iconResult = await _iconRetreiver.GetIconAsync(icon, color);
            return Ok(iconResult);
        }
    }
}
