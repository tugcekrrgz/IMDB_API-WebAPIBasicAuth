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


//CORS
builder.Services.AddCors(cors =>
{
    cors.AddPolicy("CORS", x=>
    {
        x.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
    });
});

var app = builder.Build();


app.UseCors("CORS");

app.UseRouting();

app.UseAuthorization();

app.MapControllers();


app.Run();
