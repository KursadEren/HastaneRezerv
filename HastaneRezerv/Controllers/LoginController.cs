
using HastaneRezerv.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace HastaneRezerv.Controllers
{
    public class LoginController : Controller
    {
        private readonly HastaneContext k;
        private readonly UserManager<RegisterModelcs> _userManager;
        private readonly SignInManager<RegisterModelcs> _signInManager;


        public LoginController(HastaneContext context, UserManager<RegisterModelcs> userManager, SignInManager<RegisterModelcs> signInManager)
        {
            k = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost]
        
        public IActionResult LoginAdmin()
        {

            return RedirectToAction("DoktorSec");
        }
        
        public IActionResult DoktorSec()
        {
            // Hastane sayfasının işlemleri
            return View();
        }

        public IActionResult OnlineRandevu()
        {
            // Hastane sayfasının işlemleri
            return View();
        }

        public async Task<IActionResult> LogOut()
        {
            // Hastane sayfasının işlemleri
            await RemoveSessionAndCookie();
            return RedirectToAction("Login");
        }
        private async Task RemoveSessionAndCookie()
        {
            if (HttpContext.Session.GetString("UserName") != null)
            {
                // Session varsa sil
                HttpContext.Session.Remove("UserName");
            }

            if (HttpContext.Request.Cookies.ContainsKey("UserRole"))
            {
                // Cookie varsa sil
                Response.Cookies.Delete("UserRole");
            }
        }

        public IActionResult SignIn()
        {
            // Hastane sayfasının işlemleri
            return View("./Views/SignIn/SignIn.cshtml");
        }
        [Authorize(Roles = "Admin")]
        public IActionResult AdminPanel()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AdminPanel(Kullanici model)
        {
            string kullaniciAdi = model.AdSoyad;
            string sifre = model.Sifre;

            // Aktif durumu sabitini tanımlayın
            const string AktifDurumu = "Aktif";

            var kullanici = (from Kullanici in k.Kullanici
                             join aktiflik in k.Aktiflik on Kullanici.AktiflikId equals aktiflik.AktiflikId
                             where Kullanici.AdSoyad == kullaniciAdi && Kullanici.Sifre == sifre &&
                                   aktiflik.Durum == AktifDurumu
                             select Kullanici).FirstOrDefault();

            if (kullanici != null)
            {
                var result = await _signInManager.PasswordSignInAsync(kullanici.AdSoyad, kullanici.Sifre, false, lockoutOnFailure: false);


                if (result.Succeeded)
                {
                    var user = await _userManager.FindByNameAsync(kullanici.AdSoyad);
                    var Roles = await _userManager.GetRolesAsync(user);

                    // Giriş başarılıysa kullanıcı bilgilerini saklama
                    SetUserSession(model.AdSoyad);

                    if (kullanici.UnvanId == 2)
                    {
                        // Admin rolüne sahipse AdminPanel'e yönlendirme
                        TempData["hata"] = "hata";
                        SetUserRoleCookie("2");
                        return View("./Views/Login/AdminPanel.cshtml");
                    }

                    // Diğer durumlar için ana sayfaya yönlendirme
                    SetUserRoleCookie("1");
                    return RedirectToAction("Index", "Home");
                }
                TempData["hata"] = "hata";
                return View("Login");
            }

            // Giriş başarısızsa veya yetkisi yoksa hata mesajı verme
            TempData["hata"] = "Giriş başarısız veya yetkisiz giriş!";
            return View("Login");
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
        private void SetUserRoleCookie(string role)
        {
            var cokOpt = new CookieOptions
            {
                Expires = DateTime.Now.AddMinutes(10)
            };

            HttpContext.Response.Cookies.Append("UserRole", role, cokOpt);
            
        }

        private void SetUserSession(string username)
        {
            HttpContext.Session.SetString("SessionUsrObject", username);
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
