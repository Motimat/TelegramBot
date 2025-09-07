using Microsoft.EntityFrameworkCore;
using TelegramBot.Entities.Tables;

namespace TelegramBot.Context
{
    public class TelegramContext : DbContext
    {
        public TelegramContext()
        {

        }

        public DbSet<TelegramUser> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlite($"Data Source=telegram.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
