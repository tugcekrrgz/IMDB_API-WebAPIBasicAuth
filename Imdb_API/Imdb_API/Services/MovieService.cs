using Imdb_API.DTOs;
using Imdb_API.Models;
using Imdb_API.Repositories;
using Microsoft.AspNetCore.Mvc;

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

        public MovieDTO RandomMovie()
        {
            Random rnd = new Random();
            var randomNumber = rnd.Next(0,_context.Movies.Count());

            var movie=_context.Movies.Where(x => x.Id==randomNumber).Select(x=> new MovieDTO
            {
                Id=x.Id,
                Title=x.Title,
                Year=x.Year,
                Description=x.Description,
                Rating=x.Rating
            }).FirstOrDefault();

            return movie;
        }

        public List<MovieDTO> SearchMovies(string value)
        {
            var movies = _context.Movies.Where(x=> x.Title.Contains(value)).Select(x => new MovieDTO
            {
                Id = x.Id,
                Title = x.Title,
                Year = x.Year,
                Description = x.Description,
                Rating = x.Rating
            }).ToList();

            return movies;
        }
    }
}
