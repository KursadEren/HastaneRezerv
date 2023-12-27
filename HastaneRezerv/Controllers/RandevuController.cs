using HastaneRezerv.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HastaneRezerv.Controllers
{
    public class RandevuController : Controller
    {
        private readonly HastaneContext _context;

        public RandevuController(HastaneContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        
        public IActionResult DoktorSec2(Doktor Model)
        {
          
            ViewBag.TarihSecenekleri = GetTarihSecenekleri();
            ViewBag.AdSoyadList = GetAdSoyad(Model);
            
            return View();
        }
        public IActionResult DoktorSec()
        {

            ViewBag.AnaBilimDaliList = GetAnaBilimDali();
            ViewBag.AktiflikList = GetAktiflik();
            ViewBag.PoliklinikList = GetPoliklinik();
            return View();
        }
        public async Task<IActionResult> OnlineRandevu(RandevuOlustur model)
        {
            try
            {
                // Perform your query using DoktorIdw
                var randevular = await _context.Randevu
                    .Where(r => r.DoktorId == model.Doktor.DoktorId && r.Tarih == DateTime.Parse(model.Tarih))                    
                    .ToListAsync();
               
                ViewBag.GetDoktor = GetDoktor(model.Doktor.DoktorId);
               
                ViewBag.GetTarih = model.Tarih;
                // Pass the result to the view
                ViewBag.Randevular = randevular;
                return View(randevular);
            }
            catch (Exception ex)
            {
                TempData["hata"] = "Sorgu sırasında bir hata oluştu: " + ex.Message;
                return View();
            }
        }
        public IActionResult Tamamla(string secilenSaat)
        {
            return View();
        }
        private List<SelectListItem> GetAktiflik()
        {
            // Veritabanından hastane verilerini çek
            var Aktiflik = _context.Aktiflik
                .Select(h => new SelectListItem { Value = h.AktiflikId.ToString(), Text = h.Durum })
                .ToList();

            return Aktiflik;
        }

        private List<SelectListItem> GetPoliklinik()
        {
            // Veritabanından hastane verilerini çek
            var Poliklinik = _context.Poliklinik
                .Select(h => new SelectListItem { Value = h.PoliklinikId.ToString(), Text = h.PoliklinikAdi })
                .ToList();

            return Poliklinik;
        }

        private List<SelectListItem> GetAnaBilimDali()
        {
            // Veritabanından hastane verilerini çek
            var AnaBilimDali = _context.AnaBilimDali
                .Select(h => new SelectListItem { Value = h.AnaBilimDaliId.ToString(), Text = h.AnaBilimDaliAdi })
                .ToList();

            return AnaBilimDali;
        }
      
       private List<SelectListItem> GetAdSoyad(Doktor model)
        {
            // Veritabanından doktor verilerini çek, filtrele
            var doktorList = _context.Doktor
                .Where(d => d.PoliklinikId == model.PoliklinikId && d.AnaBilimDaliId == model.AnaBilimDaliId)
                .Select(d => new SelectListItem { Value = d.DoktorId.ToString(), Text = d.AdSoyad })
                .ToList();
            if(doktorList.Count > 0)
            return doktorList;
            return null;
        }
        private SelectListItem GetDoktor(int model)
        {
            // Veritabanından doktor verisini çek
            var doktor = _context.Doktor
                .FirstOrDefault(d => d.DoktorId == model);

            if (doktor != null)
            {
                // Belirli bir doktor bulunduğunda, SelectListItem'i oluştur ve döndür
                return new SelectListItem
                {
                    Text = doktor.AdSoyad, // Sadece Text özelliği dolduruluyor
                    Value = doktor.DoktorId.ToString() // Ancak arka planda Value değeri de alınabilir
                };
            }
            else
            {
                // Belirli bir DoktorId ile eşleşen doktor bulunamadı.
                return new SelectListItem { Text = "", Value = "" };
            }
        }


        private List<SelectListItem> GetTarihSecenekleri()
        {
            var baslangicTarihi = DateTime.Now;
            var bitisTarihi = baslangicTarihi.AddDays(7); // 1 hafta sonrası

            var tarihler = new List<SelectListItem>();

            while (baslangicTarihi <= bitisTarihi)
            {
                tarihler.Add(new SelectListItem
                {
                    Value = baslangicTarihi.ToString("yyyy-MM-dd"),
                    Text = baslangicTarihi.ToString("dd/MM/yyyy")
                });

                baslangicTarihi = baslangicTarihi.AddDays(1);
            }

            return tarihler;
        }


    }
}
