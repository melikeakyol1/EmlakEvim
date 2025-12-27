namespace emlakdeneme.Models
{
    public class Ilan
    {
        public int Id { get; set; }
        public string Baslik { get; set; }
        public string Durum { get; set; } // Satılık / Kiralık
        public string Tip { get; set; }   // Daire, Arsa, Dükkan
        public string Sehir { get; set; }
        public string Semt { get; set; }
        public string Mahalle { get; set; }
        public decimal Fiyat { get; set; }
        public int Metrekare { get; set; }
        public int Oda { get; set; }
        public string Resim { get; set; }

        public bool Onaylandi { get; set; } = false;

        public int? KullaniciId { get; set; }    // nullable yaptık
        public Kullanici? Kullanici { get; set; } // nullable yaptık

        public ICollection<IlanResim> IlanResimler { get; set; }

        public bool SatildiMi { get; set; } = false;      // Satıldı mı?
        public bool KiralandiMi { get; set; } = false;    // Kiralandı mı?
    }
}
