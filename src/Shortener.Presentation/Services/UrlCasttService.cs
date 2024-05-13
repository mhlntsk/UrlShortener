namespace Shortener.Presentation.Services
{
    public class UrlCasttService
    {
        private readonly IConfiguration configuration;
        public UrlCasttService(IConfiguration configuration)
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
