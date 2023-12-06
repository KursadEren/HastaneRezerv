using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
namespace HastaneRezerv.Models
{
    public class HastaneContext : DbContext
    {
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
            optionsBuilder.UseSqlServer(@"Server=KURSAD;Database=Hastanedb;Integrated Security=True;");
        }
    }
}

