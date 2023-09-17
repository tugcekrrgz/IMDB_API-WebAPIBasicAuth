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

        public List<MovieDTO> After2015()
        {
            var movies = _context.Movies.Where(x => x.Year >= 2015).Select(x => new MovieDTO
            {
                Id = x.Id,
                Title=x.Title,
                Description=x.Description,
                Year = x.Year,
                Rating = x.Rating
            }).OrderByDescending(x => x.Year).ToList();
            return movies;
        }

        public List<MovieDTO> Before2015()
        {
            var movies = _context.Movies.Where(x => x.Year < 2015).Select(x => new MovieDTO
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                Year = x.Year,
                Rating = x.Rating
            }).OrderByDescending(x=> x.Year).ToList();
            return movies;
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
            }).OrderByDescending(x => x.Year).ToList();

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

        public List<MovieDTO> RatingAbove70()
        {
            var movies = _context.Movies.Where(x => x.Rating >= 70).Select(x => new MovieDTO
            {
                Id = x.Id,
                Title=x.Title,
                Description=x.Description,
                Rating=x.Rating,
                Year=x.Year
            }).OrderByDescending(x => x.Rating).ToList();
            return movies;
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
