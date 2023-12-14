using HastaneRezerv.Models;
using Microsoft.AspNetCore.Mvc;

namespace HastaneRezerv.Controllers
{
    public class SignInController : Controller
    {
        private  HastaneContext _context = new HastaneContext();

       

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SignIn(Kullanici model)
        {
            if (_context.Kullanici.Any(u => u.TcNo == model.TcNo && u.AdSoyad == model.AdSoyad))
            {
                TempData["hata"] = "Bu TcNo ve AdSoyad ile kayıtlı bir kullanıcı zaten var";
                return View();
            }
            if (model.Sifre == model.TekrarSifre)
            {
                model.UnvanId = 1;
                // Kullanıcıyı veritabanına ekle
                _context.Kullanici.Add(model);
                _context.SaveChanges();
                TempData["hata"] = "Kayıt işlemi gerçekleştirildi";
                return RedirectToAction("Login" , "Login");
            }
            
           
            return View();
        }

    }
}
