using CleanArchitecture.Data;
using CleanArchitecture.Domain;

StreamerDbContext dbContext = new();

//await AddNewRecords();
QueryStreaming();
void QueryStreaming()
{
    var streamers = dbContext!.Streamers!.ToList();
    foreach (var streamer in streamers)
    {
        Console.WriteLine($"Nombre: {streamer.Name} (Id: {streamer.Id})");
    }
}

async Task AddNewRecords()
{
    Streamer streamer = new()
    {
        Name = "Disney",
        Url = "https://www.disney.com"
    };

    dbContext!.Streamers!.Add(streamer);
    await dbContext.SaveChangesAsync();

    var movies = new List<Video>
{
    new Video { Name = "La cenicienta", StreamerId = streamer.Id },
    new Video { Name = "101 Dalmatas", StreamerId = streamer.Id },
    new Video { Name = "La bella y la bestia", StreamerId = streamer.Id },
    new Video { Name = "Star Wars", StreamerId = streamer.Id },
};

    await dbContext!.AddRangeAsync(movies);
    await dbContext.SaveChangesAsync();
}