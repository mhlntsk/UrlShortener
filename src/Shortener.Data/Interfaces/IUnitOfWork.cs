namespace Shortener.Data.Interfaces
{
    public interface IUnitOfWork
    {
        IUrlRepository UrlRepository { get; }

        Task SaveAsync();
    }
}
