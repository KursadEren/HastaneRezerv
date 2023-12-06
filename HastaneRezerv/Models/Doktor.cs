namespace HastaneRezerv.Models
{
    public class Doktor
    {
        public int  DoktorId  {  get; set; }
        public string AdSoyad { get; set; }
        public int TcNo { get; set; }
        public int AnaBilimDaliId { get; set; }
        public int PoliklinikId { get; set; }
        public AnaBilimDali AnaBilimDali { get; set; }
        public Poliklinik Poliklinik { get; set; }


    }
}
