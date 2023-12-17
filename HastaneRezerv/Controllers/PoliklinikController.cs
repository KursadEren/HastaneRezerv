using HastaneRezerv.Models;
using Microsoft.AspNetCore.Mvc;

namespace HastaneRezerv.Controllers
{
    public class PoliklinikController : Controller
    {
        private HastaneContext k = new HastaneContext(); 
        public IActionResult Index()
        {
            var y = k.Poliklinik.ToList();
            return View(y);


        }
    }
}
