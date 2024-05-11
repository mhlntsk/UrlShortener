using Microsoft.EntityFrameworkCore;
using Shortener.Data.Data;
using Shortener.Data.Entities;
using Shortener.Data.Interfaces;
using Shortener.Data.Validation;

namespace Shortener.Data.Repositories
{
    public class UrlRepository : IUrlRepository
    {
        private readonly ShortenerDbContext context;
        public UrlRepository(ShortenerDbContext context)
        {
            this.context = context;
        }
        public async Task<bool> IsUniqueShortUrlAsync(string shortUrl)
        {
            return await context.URLs.AllAsync(u => u.ShortUrl != shortUrl);
        }
        public async Task AddAsync(URL entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            await context.URLs.AddAsync(entity);
        }

        public void Delete(URL entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            context.URLs.Remove(entity);
        }

        public async Task DeleteByIdAsync(int id)
        {
            var entity = await context.URLs.FindAsync(id);
            if (entity != null)
            {
                context.URLs.Remove(entity);
            }
            else
            {
                throw new DatabaseDoesntContainException($"URL with id {id} not found");
            }
        }

        public IQueryable<URL> GetAll()
        {
            return context.URLs;
        }

        public async Task<IEnumerable<URL>> GetAllAsync()
        {
            return await context.URLs.ToListAsync();
        }

        public async Task<URL?> GetByIdAsync(int id)
        {
            return await context.URLs.FindAsync(id);
        }

        public async Task Update(URL entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var existingEntity = await GetByIdAsync(entity.Id);

            if (existingEntity == null)
            {
                throw new DatabaseDoesntContainException($"URL with id {entity.Id} not found");
            }

            context.Entry(existingEntity).CurrentValues.SetValues(entity);
        }
    }
}
