using HastaneRezerv.Models;
using Microsoft.AspNetCore.Mvc;

namespace HastaneRezerv.Controllers
{
    public class AnaBilimDaliController : Controller
    {
        private HastaneContext k = new HastaneContext();
        public IActionResult Index()
        {
            var y = k.AnaBilimDali.ToList();
            return View(y);


        }
        public IActionResult Create()
        {
            return View();
        }
    }
}
