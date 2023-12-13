
namespace HastaneRezerv.Models
{
    public class Kullanici
    {
        
        public int  KullaniciId { get; set;}
        public string  AdSoyad { get; set; }
        public int TcNo { get;set; }
        public int UnvanId { get; set; }
        public string Sifre { get; set; }
        public Unvan Unvan { get; set; }
    }
}
