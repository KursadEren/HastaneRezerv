using HastaneRezerv.Models;
using Microsoft.AspNetCore.Mvc;

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
       
        public IActionResult Detail()
        {
            return View();
        }
        public IActionResult Edit()
        {
            return View();
        }
        public IActionResult Delete(int? id)
        {

            if(id == null)
            {

                return View();
            }

            return View();

        }
    }
}
