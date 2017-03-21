
using System.Linq;

using System.Web.Mvc;
using GSMovieRental.Models;
using PagedList;

namespace GSMovieRental.Controllers
{
    public class MovieController : Controller
    {
        // GET: Movie

        private readonly  MovieDbContext movieContext = null;

        public MovieController()
        {
            movieContext = MovieDbContext.Create();
        }

        protected override void Dispose(bool disposing)
        {
            movieContext.Dispose();
        }

        public ActionResult Index(string search, string currentFilter, int pageNumber = 1)
        {
            var movies = (from m in movieContext.Movies select m);
            if (search != null)
            {
                pageNumber = 1;
            }
            else
            {
                search = currentFilter;
            }

            ViewBag.CurrentFilter = search;

            if (!string.IsNullOrEmpty(search))
            {
                
                movies = movies.Where(m => m.Title.Contains(search));
            }

            var moviesList = movies.OrderBy(m => m.Title).ToPagedList(pageNumber, 15);

            if (Request.IsAjaxRequest())
            {
                return PartialView("_SearchView", moviesList);
            }
            return View(moviesList);
            
        }
    }
}