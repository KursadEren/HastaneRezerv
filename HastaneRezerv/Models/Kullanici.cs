
namespace HastaneRezerv.Models
{
    public class Kullanici
    {
        
        public int  KullaniciId { get; set;}
        public string  AdSoyad { get; set; }
        public string TcNo { get;set; }
        public int UnvanId { get; set; }
        public string Sifre { get; set; }
        public string TekrarSifre { get; set; }
        public Unvan Unvan { get; set; }
        public ICollection<Randevu>? Randevu { get; set; }
    }
}
