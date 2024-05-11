using Shortener.Data.Interfaces;

namespace Shortener.Data.Entities
{
    public class BaseEntity : IBaseEntity
    {
        public int Id { get; set; }
    }
}
