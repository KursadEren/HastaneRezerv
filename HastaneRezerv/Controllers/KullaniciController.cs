using HastaneRezerv.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Drawing;
using static NuGet.Packaging.PackagingConstants;

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
        [HttpPost]
        public IActionResult Create(Kullanici Model)
        {
             
            if (!ModelState.IsValid)
            {
                Model.AktiflikId = 3;// default
                k.Kullanici.Add(Model);
                //k.Add(y);
                
                k.SaveChanges();
                TempData["msj"] = Model.AdSoyad + " adlı yazar eklendi";
                return RedirectToAction("Index");
            }
            TempData["msj"] = "Ekleme başarısız";

            return View(Model);

        }
        public IActionResult Create()
        {
            return View();
        }
       
        public IActionResult Details(int ?id)
        {
            var kullanici = k.Kullanici.FirstOrDefault(k => k.KullaniciId == id);
            return View(kullanici);

        }
       
        public IActionResult Edit(int ?id)
        {
            var kullanici = k.Kullanici.FirstOrDefault(k => k.KullaniciId == id);
            return View(kullanici);
           
        }
        [HttpPost]
        public async  Task<IActionResult> Edit(int ?id,[Bind("KullaniciId,AdSoyad,TcNo,Sifre,TekrarSifre,AktiflikId,UnvanId")] Kullanici Kullanici)
        {

            if (id != Kullanici.KullaniciId)
            {
                return NotFound();
            }
            try
            {
                k.Update(Kullanici);
                await k.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KullaniciExist(Kullanici.KullaniciId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
            
        }
        public IActionResult Delete()
        {

            return View();

        }
        [HttpPost]
        public IActionResult Delete(int? id)
        {

            if(id == null)
            {
                TempData["hata"] = "Id değeri yanlış";
                return View();
            }
           
            var kullanici = k.Kullanici.FirstOrDefault(k => k.KullaniciId == id);

            if (kullanici != null)
            {
                // Kullanıcının aktiflik durumunu güncelle
                var aktiflik = k.Aktiflik.FirstOrDefault(a => a.Durum == "Pasif");
                var pasifDurumId = k.Aktiflik.FirstOrDefault(a => a.AktiflikId == aktiflik.AktiflikId);

                if (aktiflik != null)
                {
                    kullanici.AktiflikId = pasifDurumId.AktiflikId; // Pasif durumu ID'sini kullanarak güncelle
                    k.SaveChanges();
                }

            }
                // Diğer işlemler

                return View();

        }
        private bool KullaniciExist(int id)
        {
            return (k.Kullanici?.Any(e => e.KullaniciId == id)).GetValueOrDefault();
        }
    }
}
