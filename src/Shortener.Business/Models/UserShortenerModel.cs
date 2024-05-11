namespace Shortener.Business.Models
{
    public class UserShortenerModel : BaseShortenerModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public IEnumerable<UrlShortenerModel> URLs { get; set; }
    }
}
