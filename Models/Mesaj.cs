namespace emlakdeneme.Models
{
    public class Mesaj
    {
        public int Id { get; set; }
        public int GonderenId { get; set; }
        public Kullanici Gonderen { get; set; }

        public int AliciId { get; set; }
        public Kullanici Alici { get; set; }

        public string Icerik { get; set; }
        public DateTime Tarih { get; set; } = DateTime.Now;
        public bool Okundu { get; set; } = false;
    }

}
