using HastaneRezerv.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HastaneRezerv.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        private readonly HastaneContext _context;

        public ApiController(HastaneContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Doktor>>> GetDoktor()
        {
            var birlesikVeri = from doktor in _context.Doktor
                               join poliklinik in _context.Poliklinik
                               on doktor.PoliklinikId equals poliklinik.PoliklinikId
                               join anabilimdali in _context.AnaBilimDali
                               on doktor.AnaBilimDaliId equals anabilimdali.AnaBilimDaliId
                               select new Doktor
                               {
                                   AdSoyad = doktor.AdSoyad,
                                   AnaBilimDali = new AnaBilimDali { AnaBilimDaliAdi = anabilimdali.AnaBilimDaliAdi },
                                   Poliklinik = new Poliklinik { PoliklinikAdi = poliklinik.PoliklinikAdi }
                                   // Diğer özellikleri de buraya ekleyebilirsiniz
                               };

            return Ok(birlesikVeri);
        }

    }
}
