using HastaneRezerv.Models;
using Microsoft.AspNetCore.Mvc;

namespace HastaneRezerv.Controllers
{
    public class AktiflikController : Controller
    {
        private HastaneContext k = new HastaneContext();
        public IActionResult Index()
        {
            var y = k.Aktiflik.ToList();
            return View(y);

        }
    }
}
