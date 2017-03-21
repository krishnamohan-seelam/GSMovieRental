using System.Data.Entity;

namespace GSMovieRental.Models
{
    public class MovieDbContext :DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<MembershipType> MembershipTypes { get; set; }
        public DbSet<Rental> Rentals { get; set; }

        public DbSet<Movie> Movies
        {
            get; set; 
            
        }

        public static MovieDbContext Create()
        {
            return new MovieDbContext();
        }
    }
}