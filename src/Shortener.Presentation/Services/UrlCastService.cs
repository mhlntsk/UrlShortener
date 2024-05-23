namespace Shortener.Presentation.Services
{
    public class UrlCastService
    {
        private readonly IConfiguration configuration;
        public UrlCastService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string CastUrl(string shortCode)
        {
            string baseUrl = configuration["domain"]!;

            return $"{baseUrl}{shortCode}";
        }
    }
}
