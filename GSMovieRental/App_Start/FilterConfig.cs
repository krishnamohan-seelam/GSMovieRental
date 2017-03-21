
using System.Web.Mvc;
using GSMovieRental.Error;

namespace GSMovieRental
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new CustomErrorHandler());

        }
    }
}
