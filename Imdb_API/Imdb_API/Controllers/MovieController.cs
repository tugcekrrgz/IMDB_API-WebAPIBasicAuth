using Imdb_API.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Imdb_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMovieRepository _movieRepository;

        public MovieController(IMovieRepository movieRepository) 
        {
            _movieRepository = movieRepository;
        }

        [HttpGet]
        public IActionResult GetMovies()
        {
            return Ok(_movieRepository.GetAllMovies());
        }

        [HttpGet("{data}")]
        public IActionResult SearchMovies(string data)
        {
            return Ok(_movieRepository.SearchMovies(data));
        }

        [HttpGet]
        [Route("RandomMovie")]
        public IActionResult RandomMovie()
        {
            var movie = _movieRepository.RandomMovie();
            return Ok(movie);
        }

        [HttpGet]
        [Route("Before2015")]
        public IActionResult Before2015()
        {
            return Ok(_movieRepository.Before2015());
        }

        [HttpGet]
        [Route("After2015")]
        public IActionResult After2015()
        {
            return Ok(_movieRepository.After2015());
        }

        [HttpGet]
        [Route("RatingAbove70")]
        public IActionResult RatingAbove70()
        {
            return Ok(_movieRepository.RatingAbove70());
        }

    }
}
