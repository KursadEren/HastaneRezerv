﻿using HastaneRezerv.Models;
using Microsoft.AspNetCore.Mvc;

namespace HastaneRezerv.Controllers
{
    public class KullaniciController : Controller
    {
        private HastaneContext k = new HastaneContext();
        public IActionResult Index()
        {
            var y = k.Kullanici.ToList();
            
            return View(y);
            
        }
    }
}
