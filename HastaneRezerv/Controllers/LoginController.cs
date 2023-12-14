
using HastaneRezerv.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using Newtonsoft.Json;
namespace HastaneRezerv.Controllers
{
    public class LoginController : Controller
    {
        private  HastaneContext _context = new HastaneContext();


       
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
        public IActionResult SignIn()
        {
            // Hastane sayfasının işlemleri
            return View("./Views/SignIn/SignIn.cshtml");
        }

        public IActionResult AdminPanel(Kullanici model)
        {
            string kullaniciAdi = model.AdSoyad;
            string sifre = model.Sifre;
          
            var kullanici = (from Kullanici in _context.Kullanici
                                   where Kullanici.AdSoyad == kullaniciAdi && Kullanici.Sifre == sifre
                                   select Kullanici).FirstOrDefault();


            if (kullanici != null)
            {
                // Giriş başarılı, istediğiniz işlemleri yapabilirsiniz
                TempData["hata"] =  sifre+ " yazı";
                SetSessinObject(kullanici);
                return View("./Views/Home/Index.cshtml");


            }
            else
            {
                // Hatalı giriş durumunda kullanıcıyı aynı sayfaya yönlendir
                TempData["hata"] = "LoginAdmin'e girmediniz2!";
                return View("Login");
            }
        }
        public IActionResult Hastane()
        {
            // Hastane sayfasının işlemleri
            return View();
        }
        public IActionResult SetSessinObject(Kullanici model)
        {
            SessionKullanici usr = new SessionKullanici();
            usr.AdSoyad = model.AdSoyad;
            usr.UnvanId = model.UnvanId;
            
            string u = JsonConvert.SerializeObject(usr);
            HttpContext.Session.SetString("SessionUsrObject", u);

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

       
        

        private string SifreHashle(string sifre)
        {
            // Bu metot, şifreyi güvenli bir şekilde hash'lemek için kullanılır
            // Bu örnekte basit bir hash algoritması kullanılmıştır (gerçek uygulamalarda daha güçlü algoritmalar kullanılmalıdır)
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(sifre));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }
    }
}
