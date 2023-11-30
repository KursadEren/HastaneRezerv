using Microsoft.AspNetCore.Mvc;

using System;

namespace HastaneRezerv.Controllers
{
    public class LoginController : Controller
    {
        [HttpPost]
        public IActionResult LoginOlustur()
        {

            return RedirectToAction("Hastane");
        }
        public IActionResult Hastane()
        {
            // Hastane sayfasının işlemleri
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }
      
    }
}
