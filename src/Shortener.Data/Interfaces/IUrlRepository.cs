using Shortener.Data.Entities;

namespace Shortener.Data.Interfaces
{
    public interface IUrlRepository : IRepository<URL>
    {
        Task<bool> IsUniqueShortUrlAsync(string shortUrl);
    }
}
