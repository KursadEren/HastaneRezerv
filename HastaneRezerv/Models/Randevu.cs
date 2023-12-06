namespace HastaneRezerv.Models
{
    public class Randevu
    {
        public int RandevuId { get; set; }
        public int DoktorId { get; set; }
        public int KullaniciId { get; set; }
        public DateTime Tarih { get; set; }

    }
}
