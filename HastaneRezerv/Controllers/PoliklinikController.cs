using HastaneRezerv.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HastaneRezerv.Controllers
{
    public class PoliklinikController : Controller
    {
        private readonly HastaneContext k;

        public PoliklinikController(HastaneContext context)
        {
            k = context;
        }

        public IActionResult Index()
        {
            var y = k.Poliklinik.ToList();
            return View(y);


        }
        [HttpPost]
        public IActionResult Create(Poliklinik Model)
        {

            if (!ModelState.IsValid)
            {
                
                k.Poliklinik.Add(Model);
                //k.Add(y);

                k.SaveChanges();
                TempData["msj"] = Model.PoliklinikAdi + " adlı yazar eklendi";
                return RedirectToAction("Index");
            }
            TempData["msj"] = "Ekleme başarısız";

            return View(Model);

        }
        public IActionResult Create()
        {
            ViewBag.HastaneList = GetHastaneList();
            ViewBag.AktiflikList = GetAktiflik();
           
            // Yeni bir Hastane modeli oluşturarak view'e gönder
            return View(new Poliklinik());


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

            var kullanici = k.Poliklinik.FirstOrDefault(k => k.PoliklinikId == id);

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
        public IActionResult Details(int? id)
        {
            var kullanici = k.Poliklinik.FirstOrDefault(k => k.PoliklinikId == id);
            return View(kullanici);

        }
        public IActionResult Edit(int? id)
        {
            var kullanici = k.Poliklinik.FirstOrDefault(k => k.PoliklinikId == id);
            return View(kullanici);

        }
        [HttpPost]
        public async Task<IActionResult> Edit(int? id, [Bind(" PoliklinikId,PoliklinikAdi ,HastaneId ,AktiflikId")] Poliklinik Poliklinik)
        {

            if (id != Poliklinik.PoliklinikId)
            {
                return NotFound();
            }
            try
            {
                k.Update(Poliklinik);
                await k.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PoliklinikExist(Poliklinik.PoliklinikId))
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
        private bool PoliklinikExist(int id)
        {
            return (k.Kullanici?.Any(e => e.KullaniciId == id)).GetValueOrDefault();
        }
        private List<SelectListItem> GetHastaneList()
        {
            // Veritabanından hastane verilerini çek
            var hastaneler = k.Hastane
                .Select(h => new SelectListItem { Value = h.HastaneId.ToString(), Text = h.HastaneAdi })
                .ToList();

            return hastaneler;
        }
        private List<SelectListItem> GetAktiflik()
        {
            // Veritabanından hastane verilerini çek
            var Aktiflik = k.Aktiflik
                .Select(h => new SelectListItem { Value = h.AktiflikId.ToString(), Text = h.Durum })
                .ToList();

            return Aktiflik;
        }

    }
}
