
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace HastaneRezerv.Models
{
    public class HastaneContext : IdentityDbContext<RegisterModelcs>
    {
        public HastaneContext(DbContextOptions<HastaneContext> options) : base(options)
        {
        }

        public DbSet<Doktor> Doktor { get; set; }
        public DbSet<Hastane> Hastane { get; set; }
        public DbSet<Poliklinik> Poliklinik { get; set; }
        public DbSet<Unvan> Unvan { get; set; }
        public DbSet<Kullanici> Kullanici { get; set; }
        public DbSet<Randevu> Randevu { get; set; }
        public DbSet<AnaBilimDali> AnaBilimDali { get; set; }
        public DbSet<Aktiflik> Aktiflik { get; set; }
        


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseSqlServer(@"Server=Kursad;Database=HastaneDataBase
;Trusted_Connection=True;MultipleActiveResultSets=true;");


        }
    }
}

