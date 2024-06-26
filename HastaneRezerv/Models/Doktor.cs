﻿namespace HastaneRezerv.Models
{
    public class Doktor
    {
        public int  DoktorId  {  get; set; }
        public string AdSoyad { get; set; }
        public int TcNo { get; set; }
        public int AnaBilimDaliId { get; set; }
        public int PoliklinikId { get; set; }
        public int AktiflikId { get; set; }
        public AnaBilimDali AnaBilimDali { get; set; }
        public Aktiflik Aktiflik { get; set; }
        public Poliklinik Poliklinik { get; set; } 
      
        public ICollection<Randevu> ?Randevu { get; set; }

       
    }
}
