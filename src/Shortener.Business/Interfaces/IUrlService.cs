using Shortener.Business.Models;

namespace Shortener.Business.Interfaces
{
    public interface IUrlService : ICrud<UrlShortenerModel>
    {
        Task<string> GetByShortedUrlAsync(string shortedUrl);
    }
}
