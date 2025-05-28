using MangoApi.Models;
using Microsoft.EntityFrameworkCore;

namespace MangoApi.DataBaseContext
{
    public class MangoDbContext : DbContext
    {
        public MangoDbContext(DbContextOptions<MangoDbContext> options) : base(options)
        {
        }

        public DbSet<User> User { get; set; }
        public DbSet<Levels> Levels { get; set; }
        public DbSet<Passinglvl> Passinglvl { get; set; }
        public DbSet<Message> Message { get; set; }

    }
}
