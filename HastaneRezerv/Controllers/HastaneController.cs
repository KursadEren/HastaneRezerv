using HastaneRezerv.Models;
using Microsoft.AspNetCore.Mvc;

namespace HastaneRezerv.Controllers
{
    public class HastaneController : Controller
    {

        private readonly HastaneContext k;

        public HastaneController(HastaneContext context)
        {
            k = context;
        }
        public IActionResult Index()
        {
            var y = k.Hastane.ToList();
            return View(y);


        }
        public IActionResult Create()
        {
          
            return View();


        }
    }
}
