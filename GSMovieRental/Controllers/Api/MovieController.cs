using System;

using System.Linq;

using System.Web.Http;
using AutoMapper;
using GSMovieRental.Dto;
using GSMovieRental.Models;

namespace GSMovieRental.Controllers.Api
{
    public class MovieController : ApiController
    {
        private readonly MovieDbContext movieContext = null;

        public MovieController()
        {
            movieContext = MovieDbContext.Create();

        }

        public IHttpActionResult GetMovies(string query = null)
        {
            var moviesQuery = movieContext.Movies.Where(m => m.CopiesAvailable > 0);

            if (!string.IsNullOrWhiteSpace(query))
                moviesQuery = moviesQuery.Where(m => m.Title.Contains(query));
            var moviesDto = moviesQuery.ToList().Select(Mapper.Map<Movie, MovieDto>);
            return Ok(moviesDto);
        }


        public IHttpActionResult GetMovie(int? id)

        {
            if (!id.HasValue)
            {
                return BadRequest();
            }
            var movie = movieContext.Movies.SingleOrDefault(c => c.Id == id.Value);
            if (movie == null)
            {
                return NotFound();
            }
            return Ok(Mapper.Map<Movie, MovieDto>(movie));

        }

        [HttpPost]
        public IHttpActionResult CreateMovie(MovieDto movieDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var movie = Mapper.Map<MovieDto, Movie>(movieDto);
            movieContext.Movies.Add(movie);
            movieContext.SaveChanges();

            movieDto.Id = movie.Id;
            return Created(new Uri(Request.RequestUri + "/" + movie.Id), movieDto);
        }
        [HttpPut]
        public IHttpActionResult UpdateMovie(int id, MovieDto movieDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var movieInDb = movieContext.Movies.SingleOrDefault(c => c.Id == id);

            if (movieInDb == null)
                return NotFound();

            Mapper.Map(movieDto, movieInDb);

            movieContext.SaveChanges();

            return Ok();
        }
    }
}
