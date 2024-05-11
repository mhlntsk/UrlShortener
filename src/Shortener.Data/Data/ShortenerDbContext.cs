using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Shortener.Data.Entities;

namespace Shortener.Data.Data
{
    public class ShortenerDbContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public ShortenerDbContext(DbContextOptions<ShortenerDbContext> options) : base(options) { }
        public ShortenerDbContext() { }

        public DbSet<URL>? URLs { get; set; }
        public DbSet<User>? Users { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<URL>(e =>
            {
                e.HasKey(r => r.Id);

                e.HasIndex(x => x.Id)
                    .IsUnique();

                e.Property(x => x.Id)
                    .ValueGeneratedOnAdd();

                e.HasOne(o => o.User)
                    .WithMany(p => p.URLs);

                e.Property(p => p.FullUrl)
                    .HasMaxLength(4000)
                    .IsRequired(true);

                e.Property(p => p.ShortUrl)
                    .HasMaxLength(40)
                    .IsRequired(true);

                e.Property(p => p.NumberOfAppeals)
                    .IsRequired(true);

                e.Property(p => p.CreatedDate)
                    .IsRequired(true);

                e.Property(p => p.LastAppeal)
                    .IsRequired(true);
            });

            builder.Entity<User>(e =>
            {
                e.HasKey(p => p.Id);

                e.HasIndex(x => x.Id)
                    .IsUnique();

                e.Property(x => x.Id)
                    .ValueGeneratedOnAdd();

                e.Property(p => p.FirstName)
                    .HasMaxLength(100)
                    .IsRequired(true);

                e.Property(p => p.LastName)
                    .HasMaxLength(100)
                    .IsRequired(false);

            });
        }
    }
}
