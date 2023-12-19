namespace HastaneRezerv.Models
{
    public class Randevu
    {
        public int RandevuId { get; set; }
        public int DoktorId { get; set; }
        public int KullaniciId { get; set; }
        public int AktiflikId { get; set; }
        public DateTime Tarih { get; set; }
        public Doktor Doktor { get; set; }
        public Kullanici Kullanici { get; set; }
        public Aktiflik Aktiflik { get; set; }

    }
}
