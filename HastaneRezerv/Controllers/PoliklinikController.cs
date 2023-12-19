using HastaneRezerv.Models;
using Microsoft.AspNetCore.Mvc;

namespace HastaneRezerv.Controllers
{
    public class PoliklinikController : Controller
    {
        private readonly HastaneContext k;

        public PoliklinikController(HastaneContext context)
        {
            k = context;
        }

        public IActionResult Index()
        {
            var y = k.Poliklinik.ToList();
            return View(y);


        }
        public IActionResult Create()
        {
           
            return View();


        }
        public IActionResult Delete()
        {
            return View();
        }
        public IActionResult Detail()
        {
            return View();
        }
        public IActionResult Edit()
        {
            return View();
        }
    }
}
