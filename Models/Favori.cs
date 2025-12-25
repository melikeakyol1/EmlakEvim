namespace emlakdeneme.Models
{
    public class Favori
    {
        public int Id { get; set; }
        public int KullaniciId { get; set; }
        public Kullanici Kullanici { get; set; }

        public int IlanId { get; set; }
        public Ilan Ilan { get; set; }

        public DateTime Tarih { get; set; } = DateTime.Now;
    }
}