namespace emlakdeneme.Models
{
    public class IlanResim
    {
        public int Id { get; set; }
        public int IlanId { get; set; }
        public Ilan Ilan { get; set; }

        public string ResimYolu { get; set; }
    }

}
