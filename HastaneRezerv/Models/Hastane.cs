namespace HastaneRezerv.Models
{
    public class Hastane
    {
        public int HastaneId { get; set; }
        public string HastaneAdi { get ; set; }
       
        public int AktiflikId { get; set; }
        public ICollection<Poliklinik> ?Poliklinik { get; set; }
        public Aktiflik Aktiflik { get; set; }
    }
}
