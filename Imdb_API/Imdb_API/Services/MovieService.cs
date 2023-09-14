using Imdb_API.DTOs;
using Imdb_API.Models;
using Imdb_API.Repositories;

namespace Imdb_API.Services
{
    public class MovieService : IMovieRepository
    {
        private readonly ImdbDataContext _context;

        public MovieService(ImdbDataContext context)
        {
            _context = context;
        }
        public List<MovieDTO> GetAllMovies()
        {
            var movies= _context.Movies.Select(x => new MovieDTO
            {
                Id = x.Id,
                Title=x.Title,
                Year = x.Year,
                Description=x.Description,
                Rating = x.Rating
            }).ToList();

            return movies;
        }
    }
}
