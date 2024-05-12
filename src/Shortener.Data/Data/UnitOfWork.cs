using Shortener.Data.Interfaces;
using Shortener.Data.Repositories;

namespace Shortener.Data.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ShortenerDbContext context;

        private IUrlRepository? urlRepository;

        public UnitOfWork(ShortenerDbContext context)
        {
            this.context = context;
        }

        public IUrlRepository UrlRepository
        {
            get
            {
                if (urlRepository == null)
                {
                    urlRepository = new UrlRepository(context);
                }
                return urlRepository;
            }
        }

        public async Task SaveAsync() => await context.SaveChangesAsync();
    }
}
