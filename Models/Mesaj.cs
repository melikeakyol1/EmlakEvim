namespace emlakdeneme.Models
{
    public class Mesaj
    {
        public int Id { get; set; }
        public int GonderenId { get; set; }
        public Kullanici Gonderen { get; set; }

        public int AliciId { get; set; }
        public Kullanici Alici { get; set; }

        public int IlanId { get; set; }      // Bu satırı ekle
        public Ilan Ilan { get; set; }       // Ilan ile ilişki

        public string Icerik { get; set; }
        public DateTime Tarih { get; set; } = DateTime.Now;
        public bool Okundu { get; set; } = false;
    }

}
