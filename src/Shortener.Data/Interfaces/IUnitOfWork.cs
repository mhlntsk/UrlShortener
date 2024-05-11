namespace Shortener.Data.Interfaces
{
    public interface IUnitOfWork
    {
        IUrlRepository UrlRepository { get; }

        IUserRepository UserRepository { get; }

        Task SaveAsync();
    }
}
