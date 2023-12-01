using Microsoft.AspNetCore.Mvc;

using System;

namespace HastaneRezerv.Controllers
{
    public class LoginController : Controller
    {
        [HttpPost]
        public IActionResult LoginOlustur()
        {

            return RedirectToAction("AdminPanel");
        }
        public IActionResult LoginAdmin()
        {

            return RedirectToAction("DoktorSec");
        }
        public IActionResult DoktorSec()
        {
            // Hastane sayfasının işlemleri
            return View();
        }
        public IActionResult AdminPanel()
        {
            // Hastane sayfasının işlemleri
            return View();
        }
        public IActionResult Hastane()
        {
            // Hastane sayfasının işlemleri
            return View();
        }

        public IActionResult SaatSec()
        {
            return RedirectToAction("Hastane");
        }
        public IActionResult Login()
        {
            return View();
        }
      
    }
}
