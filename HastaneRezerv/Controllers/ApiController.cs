﻿using HastaneRezerv.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace HastaneRezerv.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiController : Controller
    {
        private readonly HastaneContext _context;

        public ApiController(HastaneContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Kullanici>>> GetKullanici()
        {
            var kullanici = await _context.Kullanici.ToListAsync();
            return Ok(kullanici);
        }
    }
}