using Microsoft.EntityFrameworkCore;
using Shortener.Data.Data;
using Shortener.Data.Entities;
using Shortener.Data.Interfaces;
using Shortener.Data.Validation;

namespace Shortener.Data.Repositories
{
    public class UserRepository : IRepository<User>
    {
        private readonly ShortenerDbContext context;
        public UserRepository(ShortenerDbContext context)
        {
            this.context = context;
        }
        public async Task AddAsync(User entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            await context.Users.AddAsync(entity);
        }

        public void Delete(User entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            context.Users.Remove(entity);
        }

        public async Task DeleteByIdAsync(int id)
        {
            var entity = await context.Users.FindAsync(id);
            if (entity != null)
            {
                context.Users.Remove(entity);
            }
            else
            {
                throw new DatabaseDoesntContainException($"User with id {id} not found");
            }
        }

        public IQueryable<User> GetAll()
        {
            return context.Users;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await context.Users.ToListAsync();
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await context.Users.FindAsync(id);
        }

        public async Task Update(User entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var existingEntity = await GetByIdAsync(entity.Id);

            if (existingEntity == null)
            {
                throw new DatabaseDoesntContainException($"User with id {entity.Id} not found");
            }

            context.Entry(existingEntity).CurrentValues.SetValues(entity);
        }
    }
}
