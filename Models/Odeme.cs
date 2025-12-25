namespace emlakdeneme.Models
{
    public class Odeme
    {
        public int Id { get; set; }
        public int KullaniciId { get; set; }
        public decimal Tutar { get; set; }
        public DateTime Tarih { get; set; } = DateTime.Now;
        public string OdemeYontemi { get; set; } // Kredi Kartı, Havale
    }

}
