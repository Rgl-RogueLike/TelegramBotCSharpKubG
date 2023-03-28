using Microsoft.EntityFrameworkCore;

namespace TelegramBotForKubG.dbutils
{
    internal class ApplicationContext : DbContext
    {
        public DbSet<Students> Students { get; set; }
        public DbSet<Faculties> Faculties { get; set; }
        public DbSet<Codes> Codes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(
                "Host=localhost;" +
                "Port=5432;" +
                "Database=Students;" +
                "Username=postgres;" +
                "Password=qwerty");
        }
    }
}
