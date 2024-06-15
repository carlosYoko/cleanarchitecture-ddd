using CleanArchitecture.Domain;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Data
{
    public class StreamerDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=127.0.0.1,14333;Database=Streamer;User Id=sa;Password=PasswordO1.")
                .LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name }, Microsoft.Extensions.Logging.LogLevel.Information)
                .EnableSensitiveDataLogging();
        }

        public DbSet<Streamer>? Streamers { get; set; }
        public DbSet<Video>? Videos { get; set; }
    }
}
