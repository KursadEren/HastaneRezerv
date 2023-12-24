using HastaneRezerv.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HastaneRezerv.Controllers
{
    public class SignInController : Controller
    {
        private readonly HastaneContext k;
        private readonly UserManager<RegisterModelcs> _userManager;
        private readonly SignInManager<RegisterModelcs> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

       

        public SignInController(HastaneContext context, UserManager<RegisterModelcs> userManager, SignInManager<RegisterModelcs> signInManager, RoleManager<IdentityRole> roleManager)
        {
            k = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
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
        public async Task<IActionResult> SignIn(RegisterModelcs model)
        {

            if (k.Kullanici.Any(u => u.TcNo == model.TcNo && u.AdSoyad == model.Name))
            {
                TempData["hata"] = "Bu TcNo ve AdSoyad ile kayıtlı bir kullanıcı zaten var";
                return View();
            }

            if (model.sifre == model.TekrarSifre)
            {
                if (model.TcNo.Length == 11)
                {
                    var user = new Kullanici
                    {
                        UnvanId = 1,
                        AdSoyad = model.Name,
                        TcNo = model.TcNo,
                        Sifre = model.sifre,
                        TekrarSifre = model.TekrarSifre,
                        AktiflikId = 1
                    };

                    model.UserName = model.Name;
                    var result = await _userManager.CreateAsync(model, model.sifre);
                    await _userManager.AddToRoleAsync(model, "User");
                    if (result.Succeeded)
                    {
                        if (!await _roleManager.RoleExistsAsync("Admin"))
                        {
                            // "Admin" rolü henüz oluşturulmamışsa, oluşturalım.
                            await _roleManager.CreateAsync(new IdentityRole("Admin"));
                        }

                        if (!await _roleManager.RoleExistsAsync("User"))
                        {
                            // "User" rolü henüz oluşturulmamışsa, oluşturalım.
                            await _roleManager.CreateAsync(new IdentityRole("User"));
                        }
                        var user2 = await _userManager.FindByNameAsync("username");
                     
                        // Kullanıcıyı istediğiniz role atayabilirsiniz.
                        await _userManager.AddToRoleAsync(user2, "User"); // veya "User"
                        // Kullanıcıyı veritabanına ekle
                        k.Kullanici.Add(user);
                        k.SaveChanges();

                        TempData["hata"] = "Kayıt işlemi gerçekleştirildi";
                        return RedirectToAction("Login", "Login");
                    }
                    else
                    {
                        // Handle errors in result.Errors
                        TempData["hata"] = "Kayıt işlemi başarısız: " + string.Join(", ", result.Errors.Select(e => e.Description));
                        return View();
                    }
                }
                else
                {
                    TempData["hata"] = "TC numarası 11 haneli olmalıdır";
                }
            }
            else
            {
                TempData["hata"] = "Şifreler uyuşmuyor";
            }

            return View();
        }


    }
}
