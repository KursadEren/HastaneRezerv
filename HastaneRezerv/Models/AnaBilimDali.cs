namespace HastaneRezerv.Models
{
    public class AnaBilimDali
    {
        public int AnaBilimDaliId { get; set; }
        public string AnaBilimDaliAdi { get; set; }
        public int AktiflikId { get; set; }
        public Aktiflik Aktiflik { get; set; }
        public ICollection<Doktor> ?Doktor { get; set; }
    }
}
