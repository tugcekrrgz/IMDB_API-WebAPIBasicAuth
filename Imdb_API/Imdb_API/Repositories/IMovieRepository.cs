using Imdb_API.DTOs;

namespace Imdb_API.Repositories
{
    public interface IMovieRepository
    {
        List<MovieDTO> GetAllMovies();
        List<MovieDTO> SearchMovies(string value);
        MovieDTO RandomMovie();
        List<MovieDTO> Before2015();
        List<MovieDTO> After2015();
        List<MovieDTO> RatingAbove70();
    }
}
