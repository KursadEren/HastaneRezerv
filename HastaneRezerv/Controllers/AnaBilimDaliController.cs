using HastaneRezerv.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HastaneRezerv.Controllers
{
    public class AnaBilimDaliController : Controller
    {
        private readonly HastaneContext k;

        public AnaBilimDaliController(HastaneContext context)
        {
            k = context;
        }
        public IActionResult Index()
        {
            var y = k.AnaBilimDali.Include(doktor => doktor.Aktiflik);
            return View(y);


        }
        [HttpPost]
        public IActionResult Create(AnaBilimDali Model)
        {

            if (!ModelState.IsValid)
            {
                Model.AktiflikId = 1;// default
                k.AnaBilimDali.Add(Model);
                //k.Add(y);

                k.SaveChanges();
                TempData["msj"] = Model.AnaBilimDaliAdi + " adlı yazar eklendi";
                return RedirectToAction("Index");
            }
            TempData["msj"] = "Ekleme başarısız";

            return View(Model);

        }
        public IActionResult Create()
        {
            ViewBag.AktiflikList = GetAktiflik();
            return View(new AnaBilimDali());
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
            

            var kullanici = k.AnaBilimDali.FirstOrDefault(k => k.AnaBilimDaliId == id);

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
        public IActionResult Details(int? id)
        {
            var kullanici = k.AnaBilimDali.FirstOrDefault(k => k.AnaBilimDaliId == id);
            return View(kullanici);

        }
        public IActionResult Edit(int? id)
        {
            var kullanici = k.AnaBilimDali.FirstOrDefault(k => k.AnaBilimDaliId == id);
            ViewBag.AktiflikList = GetAktiflik();
            return View(kullanici);

        }
        [HttpPost]
        public async Task<IActionResult> Edit(int? id, [Bind("AnaBilimDaliId,AnaBilimDaliAdi,AktiflikId")] AnaBilimDali AnaBilimDali)
        {

            if (id != AnaBilimDali.AnaBilimDaliId)
            {
                return NotFound();
            }
            try
            {
                k.Update(AnaBilimDali);
                await k.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AnaBilimDaliIdExist(AnaBilimDali.AnaBilimDaliId))
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
        private bool AnaBilimDaliIdExist(int id)
        {
            return (k.Kullanici?.Any(e => e.KullaniciId == id)).GetValueOrDefault();
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
