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
            TempData["hata"] = Model.AnaBilimDaliId + " " + Model.PoliklinikId;
           
            
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
        public IActionResult OnlineRandevu()
        {
            var randevular = _context.Randevu.ToList();
            ViewBag.Randevular = randevular;

            return View(randevular);
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

            return doktorList;
        }

    }
}
