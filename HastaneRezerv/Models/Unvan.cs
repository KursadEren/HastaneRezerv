﻿namespace HastaneRezerv.Models
{
    public class Unvan
    {
        public int UnvanId { get; set; }
        public string UnvanAdi { get; set; }
        public ICollection<Kullanici> ?Kullanici { get; set; }
    }
}
