using HastaneRezerv.Models;
using Microsoft.AspNetCore.Mvc;

namespace HastaneRezerv.Controllers
{
    public class DoktorController : Controller
    {
        private readonly HastaneContext k;

        public DoktorController(HastaneContext context)
        {
            k = context;
        }
        public IActionResult Index()
        {
            var y = k.Doktor.ToList();
            return View(y);

            
        }
    }
}
