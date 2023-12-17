using HastaneRezerv.Models;
using Microsoft.AspNetCore.Mvc;

namespace HastaneRezerv.Controllers
{
    public class HastaneController : Controller
    {

        private HastaneContext k = new HastaneContext();
        public IActionResult Index()
        {
            var y = k.Hastane.ToList();
            return View(y);


        }
    }
}
