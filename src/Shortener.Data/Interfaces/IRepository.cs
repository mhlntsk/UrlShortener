namespace Shortener.Data.Interfaces
{
    public interface IRepository<TEntity> where TEntity : IBaseEntity
    {
        Task AddAsync(TEntity entity);

        Task<TEntity?> GetByIdAsync(int id);

        Task<IEnumerable<TEntity>> GetAllAsync();

        IQueryable<TEntity> GetAll();

        Task Update(TEntity entity);

        Task DeleteByIdAsync(int id);

        void Delete(TEntity entity);
    }
}
