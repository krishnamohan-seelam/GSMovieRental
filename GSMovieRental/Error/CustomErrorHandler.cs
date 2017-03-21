using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GSMovieRental.Error
{
    public class CustomErrorHandler : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            if (!filterContext.ExceptionHandled)
            {
                string controller = filterContext.RouteData.Values["controller"].ToString();
                string action = filterContext.RouteData.Values["action"].ToString();
                Exception exception = filterContext.Exception;
                var model = new HandleErrorInfo(filterContext.Exception, "Controller", "Action");

                
                filterContext.Result = new ViewResult
                {
                    ViewName = "Error",
                    ViewData = new ViewDataDictionary(model)

                };
                //ViewBag.FileName = fileTarget.FileName.ToString();
                //  Using the target, get the full path to the log file
                //string retval = Path.GetFullPath(fileTarget.FileName.Render());
            }

        }

       
    }
}