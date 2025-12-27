using System.ComponentModel.DataAnnotations;

namespace emlakdeneme.Models.ViewModels
{
    public class SifreDegistirViewModel
    {
        [Required]
        public string EskiSifre { get; set; }

        [Required]
        public string YeniSifre { get; set; }

        [Required]
        [Compare("YeniSifre", ErrorMessage = "Şifreler uyuşmuyor")]
        public string YeniSifreTekrar { get; set; }
    }
}
