using HastaneRezerv.Models;
using Microsoft.AspNetCore.Mvc;

namespace HastaneRezerv.Controllers
{
    public class KullaniciController : Controller
    {

        private readonly HastaneContext k;

        public KullaniciController(HastaneContext context)
        {
            k = context;
        }
        public IActionResult Index()
        {
            var y = k.Kullanici.ToList();
            
            return View(y);
            
        }
        public IActionResult Create()
        {

            return View();

        }
    }
}
