using CleanArchitecture.Data;
using CleanArchitecture.Domain;
using Microsoft.EntityFrameworkCore;

StreamerDbContext dbContext = new();

await QueryFilter();
//QueryStreaming();
//await AddNewRecords();

async Task QueryFilter()
{
    Console.WriteLine("Introduce el nombre de la compañia: ");
    var companyName = Console.ReadLine();
    //var streamers = await dbContext.Streamers!.Where(s => s.Name == companyName).ToListAsync();
    // La misma consulta pero con la sintaxis de comparacion en c#
    //var streamers = await dbContext.Streamers!.Where(s => s.Name.Equals(companyName)).ToListAsync();

    // Utiliza el método Contains de C#, que se traduce en una cláusula LIKE en SQL
    //var streamers = await dbContext.Streamers!.Where(s => s.Name.Contains(companyName!)).ToListAsync();

    // Utiliza directamente la función LIKE de SQL a través de EF.Functions.Like
    var streamers = await dbContext.Streamers!.Where(s => EF.Functions.Like(s.Name, $"%{companyName}%")).ToListAsync();


    if (streamers.Any())
    {
        Console.WriteLine($"{streamers.First().Name} - {streamers.First().Url}");
        return;
    }
    Console.WriteLine("No existe la compañia");
}

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