using CleanArchitecture.Domain;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Infrastructure.Persistence
{
    internal class StreamerDbContextSeed
    {
        public static async Task SeedAsync(StreamerDbContext context, ILogger<StreamerDbContextSeed> logger)
        {
            if (!context.Streamers!.Any())
            {
                context.Streamers!.AddRange(GetPreconfiguredStreamer());
                await context.SaveChangesAsync();
                logger.LogInformation("Insertando nuevos registros en la base de datos {contezxt}", typeof(StreamerDbContext).Name);
            }
        }

        private static IEnumerable<Streamer> GetPreconfiguredStreamer()
        {
            return new List<Streamer>()
            {
                new Streamer(){CreateBy ="Carlos", Name = "Rakuten", Url = "https://www.rakuten.com"},
                new Streamer(){CreateBy ="Carlos", Name = "Movistar+", Url = "https://www.movistar.com"},
            };
        }
    }
}
