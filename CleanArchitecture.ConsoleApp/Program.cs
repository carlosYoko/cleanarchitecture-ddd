using CleanArchitecture.Data;
using CleanArchitecture.Domain;
using Microsoft.EntityFrameworkCore;

StreamerDbContext dbContext = new();

await MultipleEntitiesQuery();
//await AddNewDirectorWIthVideo();
//await AddNewActorWithVideo();
//await AddNewStreamerWithVideoId();
//await AddNewStreamerWithVideo();
//await TrackingAndNotTracking();
//await QueryLinq();
//await QueryMethods();
//await QueryFilter();
//QueryStreaming();
//await AddNewRecords();

async Task MultipleEntitiesQuery()
{
    var videoWithActor = await dbContext!.Videos!.Include(a => a.Actors).FirstOrDefaultAsync(q => q.Id == 1);

    var actor = await dbContext!.Actors!.Select(q => q.Name).ToListAsync();

    var videoWithDirector = await dbContext!.Videos!
                                    .Where(q => q.Director != null)
                                    .Include(d => d.Director)
                                    .Select(q =>
                                    new
                                    {
                                        DirectorName = $"{q.Director!.Name} {q.Director.SurName}",
                                        Movie = q.Name
                                    }
                                    ).ToListAsync();

    foreach (var movie in videoWithDirector)
    {
        Console.WriteLine($"{movie.Movie} - {movie.DirectorName}");
    }
}

async Task AddNewDirectorWIthVideo()
{
    var director = new Director() { Name = "George", SurName = "Miller", VideoId = 1 };

    dbContext.Add(director);
    await dbContext.SaveChangesAsync();
}

async Task AddNewActorWithVideo()
{
    var actor = new Actor()
    {
        Name = "Tom",
        SurName = "Hardy"
    };

    await dbContext.AddAsync(actor);
    await dbContext.SaveChangesAsync();

    var videoActor = new VideoActor() { ActorId = actor.Id, VideoId = 1 };

    await dbContext.AddAsync(videoActor);
    await dbContext.SaveChangesAsync();
}

async Task AddNewStreamerWithVideoId()
{
    var movie = new Video() { Name = "Batman Forever", StreamerId = 1002 };

    await dbContext.AddAsync(movie);
    await dbContext.SaveChangesAsync();
}

async Task AddNewStreamerWithVideo()
{
    var movistar = new Streamer() { Name = "Movistar", Url = "https://www.movistar.com" };
    var movie = new Video() { Name = "Juegos del hambre", Streamer = movistar };

    await dbContext.AddAsync(movie);
    await dbContext.SaveChangesAsync();
}
async Task TrackingAndNotTracking()
{
    var stramer = await dbContext!.Streamers!.FirstOrDefaultAsync(x => x.Id == 1);
    stramer!.Name = "Netflix Super";

    var stramer2 = await dbContext!.Streamers!.AsNoTracking().FirstOrDefaultAsync(x => x.Id == 2);
    stramer2!.Name = "Amazon Plus";

    await dbContext.SaveChangesAsync();

}

async Task QueryLinq()
{
    var streamers = await (from i in dbContext.Streamers
                           where EF.Functions.Like(i.Name, "%a%")
                           select i).ToListAsync();

    foreach (var streamer in streamers)
    {
        Console.WriteLine($"{streamer.Name} - {streamer.Url}");
    }
}

async Task QueryMethods()
{
    // Si no existe el resultado de la condicion dispara una excepcion (detiene la ejecucion del programa)
    var streamer1 = await dbContext.Streamers!.Where(s => s.Name.Contains("a")).FirstAsync();

    // Si no existe el resultado de la condicion devuelve un valor por defecto en null (no detiene la ejecucion del programa)
    var streamer2 = await dbContext.Streamers!.Where(s => s.Name.Contains("a")).FirstOrDefaultAsync();
    // Misma consulta pero mas eficiente ya que combina el filtrado y la seleccion del primer elemento en una misma operacion.
    var streamer3 = await dbContext.Streamers!.FirstOrDefaultAsync(s => s.Name.Contains("a"));

    // Si el resultado de la condicion devuelve mas de 1 resultado o ninguno disparará un error.
    var streamer4 = await dbContext.Streamers!.Where(s => s.Id == 1).SingleAsync();

    // Lo mismo pero devuelve un valor por defecto en null si no encuentra resultado pero disparara un error si encuentra 2
    var streamer5 = await dbContext.Streamers!.Where(s => s.Id == 1).SingleOrDefaultAsync();

    // Query para buscar un registro por su ID
    var streamer6 = await dbContext.Streamers!.FindAsync(1);
}

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