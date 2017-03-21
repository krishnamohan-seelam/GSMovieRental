using System;

using System.Linq;

using System.Web.Http;
using GSMovieRental.Dto;
using GSMovieRental.Models;

namespace GSMovieRental.Controllers.Api
{
    public class NewRentalController : ApiController
    {
        private readonly  MovieDbContext movieContext;

        public NewRentalController()
        {
            movieContext = MovieDbContext.Create();

        }
        [HttpPost]
        public IHttpActionResult CreateNewRental(NewRentalDto newRental)
        {
            var customer  = movieContext.Customers.Single(c=>c.Id == newRental.CustomerId);
            var movies = movieContext.Movies.Where(m => newRental.MovieIds.Contains(m.Id));

            foreach (var movie in movies)
            {
                movie.CopiesAvailable--;
                var rental = new Rental
                {
                    Customer = customer,
                    Movie = movie,
                    DateRented = DateTime.Now
                };
                    
                 
                movieContext.Rentals.Add(rental);
               

            }
            movieContext.SaveChanges();
            return Ok();
        }

    }
}
