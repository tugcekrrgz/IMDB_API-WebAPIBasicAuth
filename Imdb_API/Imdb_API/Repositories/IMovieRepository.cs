using Imdb_API.DTOs;

namespace Imdb_API.Repositories
{
    public interface IMovieRepository
    {
        List<MovieDTO> GetAllMovies();
        List<MovieDTO> SearchMovies(string value);
        MovieDTO RandomMovie();
    }
}
