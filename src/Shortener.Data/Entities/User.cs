using Microsoft.AspNetCore.Identity;
using Shortener.Data.Interfaces;

namespace Shortener.Data.Entities
{
    public class User : IdentityUser<int>, IBaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public IEnumerable<URL> URLs { get; set; }
    }
}
