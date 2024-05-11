using Shortener.Business.Models;
using System.Text;

namespace Shortener.Business.Tools
{
    public static class UrlHasher
    {
        private static readonly Random random = new Random();
        private const string AllPossibleCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

        public static void GenerateShortUrl(this UrlShortenerModel url, int lengthOfShortUrl = 6)
        {
            if (lengthOfShortUrl <= 0)
                throw new ArgumentException("Length of short url should be greater than zero.");

            var shortUrlBuilder = new StringBuilder();

            for (int i = 0; i < lengthOfShortUrl; i++)
            {
                shortUrlBuilder.Append(AllPossibleCharacters[random.Next(AllPossibleCharacters.Length)]);
            }

            url.ShortUrl = shortUrlBuilder.ToString();
        }
    }
}
