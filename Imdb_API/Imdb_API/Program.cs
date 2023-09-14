using Imdb_API.Models;
using Imdb_API.Repositories;
using Imdb_API.Services;

var builder = WebApplication.CreateBuilder(args);

//Controller
builder.Services.AddControllers();

//DbContext
builder.Services.AddDbContext<ImdbDataContext>();

//Services
builder.Services.AddScoped<IMovieRepository, MovieService>();

var app = builder.Build();


app.UseRouting();

app.MapControllers();





app.Run();
