namespace HastaneRezerv.Models
{
    public class Aktiflik
    {
        public int AktiflikId { get; set; }
        public string Durum { get; set; }
        public ICollection<Doktor>? Doktor { get; set; }
        public ICollection<Kullanici>? Kullanici { get; set; }
        public ICollection<Hastane>? Hastane { get; set; }
        public ICollection<Poliklinik>? Poliklinik { get; set; }
        public ICollection<Randevu>? Randevu { get; set; }
        public ICollection<AnaBilimDali>? AnaBilimDali { get; set; }


    }
}
