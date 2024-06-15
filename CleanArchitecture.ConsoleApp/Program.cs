using CleanArchitecture.Data;
using CleanArchitecture.Domain;

StreamerDbContext dbContext = new();

Streamer streamer = new()
{
    Name = "Amazon Prime",
    Url = "https://www.amazonprime.com"
};

dbContext!.Streamers!.Add(streamer);
await dbContext.SaveChangesAsync();

var movies = new List<Video>
{
    new Video { Name = "Mad Max", StreamerId = streamer.Id },
    new Video { Name = "Batman", StreamerId = streamer.Id },
    new Video { Name = "El club de la lucha", StreamerId = streamer.Id },
    new Video { Name = "Citizen Kane", StreamerId = streamer.Id },
};

await dbContext!.AddRangeAsync(movies);
await dbContext.SaveChangesAsync();