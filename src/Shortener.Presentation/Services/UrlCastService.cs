namespace Shortener.Presentation.Services
{
    public static class UrlCastServiceExtensions
    {
        private static readonly string baseUrl = "https://localhost:7286/nav/";
        public static string CastUrl(this string shortCode)
        {
            return $"{baseUrl}{shortCode}";
        }
    }

}
