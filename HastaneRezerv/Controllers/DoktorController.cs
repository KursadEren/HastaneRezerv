using HastaneRezerv.Models;
using Microsoft.AspNetCore.Mvc;

namespace HastaneRezerv.Controllers
{
    public class DoktorController : Controller
    {
        private HastaneContext k = new HastaneContext();
        public IActionResult Index()
        {
            var y = k.Doktor.ToList();
            return View(y);

            
        }
        public IActionResult Doktorlarimiz()
        {
            var y = k.Doktor.ToList();
            return View(y);


        }
    }
}
