using HastaneRezerv.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HastaneRezerv.Controllers
{
    public class DoktorController : Controller
    {
        private readonly HastaneContext k;

        public DoktorController(HastaneContext context)
        {
            k = context;
        }
        public IActionResult Index()
        {
            var y = k.Doktor.ToList();
            return View(y);

            
        }
        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]
        public IActionResult Create(Doktor Model)
        {

            if (!ModelState.IsValid)
            {
                Model.AktiflikId = 3;// default
                k.Doktor.Add(Model);
                //k.Add(y);

                k.SaveChanges();
                TempData["msj"] = Model.AdSoyad + " adlı yazar eklendi";
                return RedirectToAction("Index");
            }
            TempData["msj"] = "Ekleme başarısız";

            return View(Model);

        }
        
        public IActionResult Details(int? id)
        {
            var Doktor = k.Doktor.FirstOrDefault(k => k.DoktorId == id);
            return View(Doktor);

        }
        public IActionResult Edit(int? id)
        {
            var Doktor = k.Doktor.FirstOrDefault(k => k.DoktorId == id);
            return View(Doktor);

        }
        [HttpPost]
        public async Task<IActionResult> Edit(int? id, [Bind(" DoktorId,AdSoyad,TcNo,AnaBilimDaliId ,PoliklinikId,AktiflikId")] Doktor Doktor)
        {

            if (id != Doktor.DoktorId)
            {
                return NotFound();
            }
            try
            {
                k.Update(Doktor);
                await k.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DoktorExist(Doktor.DoktorId))
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

            if (id == null)
            {
                TempData["hata"] = "Id değeri yanlış";
                return View();
            }
            int pasifDurumId = 4;

            var kullanici = k.Doktor.FirstOrDefault(k => k.DoktorId == id);

            if (kullanici != null)
            {
                // Kullanıcının aktiflik durumunu güncelle
                var aktiflik = k.Aktiflik.FirstOrDefault(a => a.AktiflikId == kullanici.AktiflikId);

                if (aktiflik != null)
                {
                    kullanici.AktiflikId = pasifDurumId; // Pasif durumu ID'sini kullanarak güncelle
                    k.SaveChanges();
                }

            }
            // Diğer işlemler

            return View();

        }
        private bool DoktorExist(int id)
        {
            return (k.Doktor?.Any(e => e.DoktorId == id)).GetValueOrDefault();
        }
    }
}
