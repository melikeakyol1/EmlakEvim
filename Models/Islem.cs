using emlakdeneme.Models;

public class Islem
{
    public int Id { get; set; }
    public int IlanId { get; set; }
    public Ilan Ilan { get; set; }

    public int KullaniciId { get; set; }  // İşlemi yapan kullanıcı
    public Kullanici Kullanici { get; set; }

    public DateTime Tarih { get; set; } = DateTime.Now;
    public string IslemTipi { get; set; } // Satın Alma / Kiralama
    public string OdemeTipi { get; set; } // Kredi Kartı / Havale / Kapıda Ödeme
    public string OdemeDurumu { get; set; } = "Başarılı"; // Simülasyon için varsayılan başarılı

}
