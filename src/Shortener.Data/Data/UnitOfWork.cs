using Shortener.Data.Interfaces;
using Shortener.Data.Repositories;

namespace Shortener.Data.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ShortenerDbContext context;

        private IUrlRepository? urlRepository;

        private IUserRepository? userRepository;

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

        public IUserRepository UserRepository
        {
            get
            {
                if (userRepository == null)
                {
                    userRepository = new UserRepository(context);
                }
                return userRepository;
            }
        }

        public async Task SaveAsync() => await context.SaveChangesAsync();
    }
}
