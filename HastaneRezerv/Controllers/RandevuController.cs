using HastaneRezerv.Models;
using Microsoft.AspNetCore.Mvc;
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
        public IActionResult DoktorSec()
        {
            return View();
        }
        public IActionResult OnlineRandevu()
        {
            var randevular = _context.Randevu.ToList();
            ViewBag.Randevular = randevular;

            return View(randevular);
        }
    }
}
