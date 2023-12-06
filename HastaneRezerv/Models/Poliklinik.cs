namespace HastaneRezerv.Models
{
    public class Poliklinik
    {
        public int PoliklinikId { get; set; }
        public string PoliklinikAdi { get; set; }
        public int HastaneId { get; set; }
        public ICollection<Doktor>? Doktor { get; set; }
        public Hastane Hastane { get; set; }
    }
}
