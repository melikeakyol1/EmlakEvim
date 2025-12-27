namespace emlakdeneme.Models
{
    public class Kullanici
    {
        public int Id { get; set; }
        public string AdSoyad { get; set; }
        public string Email { get; set; }
        public string Sifre { get; set; }
        public string Telefon { get; set; }
        public string Sehir { get; set; }
        public string Adres { get; set; }
        public int RolId { get; set; } = 2; // Varsayılan olarak 2 (Kullanıcı)
        public virtual Rol Rol { get; set; }

    }
}