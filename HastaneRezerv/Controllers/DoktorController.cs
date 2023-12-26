using HastaneRezerv.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Net.Http;
namespace HastaneRezerv.Controllers
{
    public class DoktorController : Controller
    {
        private readonly HastaneContext k;
        private readonly IHttpClientFactory _httpClientFactory;

        
        public DoktorController(HastaneContext context, IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            k = context;
        }
        public async  Task<IActionResult> Index()
        {
            var HastaneContext = k.Doktor
             .Include(doktor => doktor.AnaBilimDali) // AnaBilimDali ile ilişkilendir
             .Include(doktor => doktor.Poliklinik);

            return View(await HastaneContext.ToListAsync());
            
 
        }

        public async Task<IActionResult> Doktorlarimiz()
        {
            string apiUrl = "https://localhost:7266/api/Api";

            using (var httpClient = _httpClientFactory.CreateClient())
            {
                HttpResponseMessage response = await httpClient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string responseData = await response.Content.ReadAsStringAsync();
                    var apiResponseModel = JsonConvert.DeserializeObject<List<Doktor>>(responseData);

                    return View(apiResponseModel);
                }
                else
                {
                    var apiResponseModel = new List<Doktor>();
                    return View(apiResponseModel);
                }
            }
        }

        public IActionResult Create()
        {
           
            ViewBag.AnaBilimDaliList = GetAnaBilimDali();
             ViewBag.AktiflikList = GetAktiflik();
            ViewBag.PoliklinikList = GetPoliklinik();
            return View(new Doktor());
           
        }
        [HttpPost]
        public IActionResult Create(Doktor Model)
        {

            if (!ModelState.IsValid)
            {
               
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
            ViewBag.AnaBilimDaliList = GetAnaBilimDali();
            ViewBag.AktiflikList = GetAktiflik();
            ViewBag.PoliklinikList = GetPoliklinik();
            return View(Doktor);

        }
        [HttpPost]
        public async Task<IActionResult> Edit(int? id, [Bind("DoktorId, AdSoyad,TcNo,AnaBilimDaliId ,PoliklinikId,AktiflikId")] Doktor Doktor)
        {
            TempData["hata"] = Doktor.PoliklinikId + " " + Doktor.AnaBilimDaliId + " " + Doktor.DoktorId;
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

        private List<SelectListItem> GetAktiflik()
        {
            // Veritabanından hastane verilerini çek
            var Aktiflik = k.Aktiflik
                .Select(h => new SelectListItem { Value = h.AktiflikId.ToString(), Text = h.Durum })
                .ToList();

            return Aktiflik;
        }

        private List<SelectListItem> GetPoliklinik()
        {
            // Veritabanından hastane verilerini çek
            var Poliklinik = k.Poliklinik
                .Select(h => new SelectListItem { Value = h.PoliklinikId.ToString(), Text = h.PoliklinikAdi })
                .ToList();

            return Poliklinik;
        }

        private List<SelectListItem> GetAnaBilimDali()
        {
            // Veritabanından hastane verilerini çek
            var AnaBilimDali = k.AnaBilimDali
                .Select(h => new SelectListItem { Value = h.AnaBilimDaliId.ToString(), Text = h.AnaBilimDaliAdi })
                .ToList();

            return AnaBilimDali;
        }
    }
}
