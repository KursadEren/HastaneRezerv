using HastaneRezerv.Models;
using Microsoft.AspNetCore.Mvc;

namespace HastaneRezerv.Controllers
{
    public class UnvanController : Controller
    {
        private HastaneContext k = new HastaneContext();
        public IActionResult Index()
        {
            var y = k.Unvan.ToList();
            return View(y);


        }
    }
}
