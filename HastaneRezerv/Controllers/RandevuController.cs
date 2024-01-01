using HastaneRezerv.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

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
                // Kontrol noktalarını ekle
                if (model.Doktor == null || string.IsNullOrEmpty(model.Tarih))
                {
                    TempData["hata"] = "Geçersiz form verileri.";
                    return RedirectToAction("ErrorPage"); // Hata sayfasına yönlendirme örneği
                }

                // Perform your query using DoktorIdw
                  // Seçilen tarihin gün sonu (18:00)

                DateTime parsedTarih = DateTime.Parse(model.Tarih);
                DateTime startOfDay = parsedTarih.Date.AddHours(8);  // Seçilen tarihin gün başlangıcı (08:00)
                DateTime endOfDay = parsedTarih.Date.AddHours(18);   // Seçilen tarihin gün sonu (18:00)
                var Id = model.Doktor.DoktorId;
                var randevular = await _context.Randevu
                    .Where(r => r.DoktorId == Id &&
                                r.Tarih >= startOfDay &&
                                r.Tarih <= endOfDay)
                    .ToListAsync();

                TempData["hata"] = DateTime.Parse(model.Tarih);
                ViewBag.GetDoktor = GetDoktor(model.Doktor.DoktorId);
                ViewBag.GetTarih = model.Tarih;
                var resultGroups = randevular; // Bu metodunuzu gerçek veri alımınıza göre uyarlayın

                // JSON formatına çevir
                var serializedData = JsonConvert.SerializeObject(resultGroups, Formatting.None,
                    new JsonSerializerSettings
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    });

                // ViewData veya ViewBag üzerinden view'e iletebilirsiniz
                ViewData["SerializedData"] = serializedData;
                return View(randevular);
            }
            catch (Exception ex)
            {
                TempData["hata"] = "Sorgu sırasında bir hata oluştu: " + ex.Message;
                return View();
            }   
        }

        private object GetResultGroups()
        {
            throw new NotImplementedException();
        }

        public IActionResult Tamamala()
        {
            var y = _context.Randevu
                .Include(doktor => doktor.Aktiflik)
                .Include(doktor => doktor.Kullanici)
                .Include(doktor => doktor.Doktor);

            return View(y);
        }
        public async Task<IActionResult> Tamamla(RandevuTamamlaModel model)
        {
           
                Randevu randevu = new Randevu();

                // Doktor tablosundan BelirliBirDoktor adına sahip olan doktorun ID'sini almak için LINQ sorgusu
                randevu.DoktorId = await _context.Doktor
                    .Where(d => d.AdSoyad == model.SelectedDoktor)
                    .Select(d => d.DoktorId)
                    .FirstOrDefaultAsync();

                randevu.tarihstring = model.SelectedTarih;
                randevu.saatstring = model.SelectedTime;
                randevu.Tarih = DateTime.ParseExact($"{model.SelectedTarih} {model.SelectedTime}", "yyyy-MM-dd HH:mm", null);

                randevu.AktiflikId = await _context.Aktiflik
                    .Where(d => d.Durum == "Aktif")
                    .Select(d => d.AktiflikId)
                    .FirstOrDefaultAsync();

            var userId = HttpContext.Session.GetInt32("USERID");

            randevu.KullaniciId = Convert.ToInt32(userId);

                _context.Randevu.Add(randevu);
                await _context.SaveChangesAsync();
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
