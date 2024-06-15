using CleanArchitecture.Data;
using CleanArchitecture.Domain;
using Microsoft.EntityFrameworkCore;

StreamerDbContext dbContext = new();

//await QueryLinq();
//await QueryMethods();
//await QueryFilter();
//QueryStreaming();
//await AddNewRecords();

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