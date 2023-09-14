using Imdb_API.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Imdb_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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
    }
}
