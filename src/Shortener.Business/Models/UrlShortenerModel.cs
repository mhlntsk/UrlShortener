namespace Shortener.Business.Models
{
    public class UrlShortenerModel : BaseShortenerModel
    {
        public string FullUrl { get; set; }
        public string ShortUrl { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastAppeal { get; set; }
        public int NumberOfAppeals { get; set; }
        public int UserId { get; set; }
    }
}
