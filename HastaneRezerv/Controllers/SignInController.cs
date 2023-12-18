using HastaneRezerv.Models;
using Microsoft.AspNetCore.Mvc;

namespace HastaneRezerv.Controllers
{
    public class SignInController : Controller
    {
        private readonly HastaneContext k;

        public SignInController(HastaneContext context)
        {
            k = context;
        }
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
            if (k.Kullanici.Any(u => u.TcNo == model.TcNo && u.AdSoyad == model.AdSoyad))
            {
                TempData["hata"] = "Bu TcNo ve AdSoyad ile kayıtlı bir kullanıcı zaten var";
                return View();
            }
            if (model.Sifre == model.TekrarSifre)
            {
                if (model.TcNo.Length == 11)
                {
                    model.UnvanId = 1;
                    // Kullanıcıyı veritabanına ekle
                    k.Kullanici.Add(model);
                    k.SaveChanges();
                    TempData["hata"] = "Kayıt işlemi gerçekleştirildi";
                    return RedirectToAction("Login", "Login");
                }
                else
                    TempData["hata"] = "TC numarası 11 haneli olmalıdır";
            }
            else
            {
                TempData["hata"] = "Şifreler uyuşmuyor";
            }
            
           
            return View();
        }

    }
}
