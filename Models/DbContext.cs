using Microsoft.EntityFrameworkCore;

namespace emlakdeneme.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Ilan> Ilanlar { get; set; }
        public DbSet<Slider> Slaytlar { get; set; }
        public DbSet<Kullanici> Kullanicilar { get; set; }
        public DbSet<Favori> Favoriler { get; set; }
        public DbSet<Mesaj> Mesajlar { get; set; }
        public DbSet<Odeme> Odemeler { get; set; }
        public DbSet<Rol> Roller { get; set; }
        public DbSet<IlanResim> IlanResimler { get; set; }
        public DbSet<Tip> Tipler { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Mesaj tablosu: cascade delete çakışmasını önle
            modelBuilder.Entity<Mesaj>()
                .HasOne(m => m.Gonderen)
                .WithMany()
                .HasForeignKey(m => m.GonderenId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Mesaj>()
                .HasOne(m => m.Alici)
                .WithMany()
                .HasForeignKey(m => m.AliciId)
                .OnDelete(DeleteBehavior.Restrict);

            // Decimal tipleri için SQL tipi belirt
            modelBuilder.Entity<Ilan>()
                .Property(i => i.Fiyat)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Odeme>()
                .Property(o => o.Tutar)
                .HasColumnType("decimal(18,2)");

        }
    }
}
