using HastaneRezerv.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HastaneRezerv.Controllers
{
    public class HastaneController : Controller
    {

        private readonly HastaneContext k;

        public HastaneController(HastaneContext context)
        {
            k = context;
        }

        public IActionResult Details(int? id)
        {
            var kullanici = k.Hastane.FirstOrDefault(k => k.HastaneId == id);
            return View(kullanici);

        }

        public async  Task<IActionResult> Index()
        {
            var HastaneContext = k.Hastane
            .Include(Hastane => Hastane.Aktiflik); // AnaBilimDali ile ilişkilendir
            
            return View(await HastaneContext.ToListAsync());


        }
        [HttpPost]
        public IActionResult Create(Hastane Model)
        {

            if (!ModelState.IsValid)
            {
              
                k.Hastane.Add(Model);
                //k.Add(y);

                k.SaveChanges();
                TempData["msj"] = Model.HastaneAdi + " adlı yazar eklendi";
                return RedirectToAction("Index");
            }
            TempData["msj"] = "Ekleme başarısız";

            return View(Model);

        }
        public IActionResult Create()
        {
            ViewBag.AktiflikList = GetAktiflik();
            return View(new Hastane());
      
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

            var kullanici = k.Hastane.FirstOrDefault(k => k.HastaneId == id);

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
        
        public IActionResult Edit(int? id)
        {
            var kullanici = k.Hastane.FirstOrDefault(k => k.HastaneId == id);
            ViewBag.AktiflikList = GetAktiflik();
           
            return View(kullanici);

        }
        [HttpPost]
        public async Task<IActionResult> Edit(int? id, [Bind("HastaneId,HastaneAdi,AktiflikId")] Hastane Hastane)
        {

            if (id != Hastane.HastaneId)
            {
                return NotFound();
            }
            try
            {
                k.Update(Hastane);
                await k.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HastaneExist(Hastane.HastaneId))
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
        private bool HastaneExist(int id)
        {
            return (k.Hastane?.Any(e => e.HastaneId == id)).GetValueOrDefault();
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
