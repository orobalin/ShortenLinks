using Microsoft.EntityFrameworkCore;

namespace ShortenLinks
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) { }

        public DbSet<ShortenLink> ShortenLinks { get; set; }

        public DbSet<ShortenLinkLog> ShortenLinkLogs { get; set; }
    }
}