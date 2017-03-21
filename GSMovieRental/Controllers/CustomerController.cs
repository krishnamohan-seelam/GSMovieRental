using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using GSMovieRental.Models;
using System.Data.Entity;
using System.Globalization;
using GSMovieRental.ViewModels;

namespace GSMovieRental.Controllers
{
    public class CustomerController : Controller
    {

        private readonly MovieDbContext movieContext = null;

        public CustomerController()
        {
            movieContext = MovieDbContext.Create();

        }
        public ActionResult Index()
        {
            IEnumerable<Customer> customers = movieContext.Customers.Include(c => c.MembershipType).Where(c=>c.IsDeleted == false).ToList();
            return View(customers);
            
        }
        protected override void Dispose(bool disposing)
        {
            movieContext.Dispose();
        }

        public ActionResult Details(int? id)

        {
            if (!id.HasValue)
            {
                return HttpNotFound();
            }
            var customer = movieContext.Customers.SingleOrDefault(c => c.Id == id.Value);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);

        }

        public ActionResult Create()

        {
            var membershipTypes = movieContext.MembershipTypes.ToList();
            var customerFormViewModel = new CustomerFormViewModel {MembershipTypes = membershipTypes , Customer = new Customer()};
            return View( "CustomerForm",customerFormViewModel);
        }

        //Customer is part of CreateCustomerViewModel
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Customer customer)
        {

          //  ModelState.SetModelValue("customer.Id", new ValueProviderResult(customer.Id,customer.Id.ToString(), CultureInfo.InvariantCulture));
           // ModelState.SetModelValue("customer.IsDeleted", new ValueProviderResult(customer.IsDeleted, customer.IsDeleted.ToString(), CultureInfo.InvariantCulture));

            if (!ModelState.IsValid)
            {
                var membershipTypes = movieContext.MembershipTypes.ToList();
                var customerFormViewModel = new CustomerFormViewModel { MembershipTypes = membershipTypes  ,Customer =customer};
                return View("CustomerForm", customerFormViewModel);
            }
            if (customer.Id == 0)
            {
                customer.IsDeleted = false;
                movieContext.Customers.Add(customer);
            }
            else
            {
                var customerFromDb = movieContext.Customers.Single(c => c.Id == customer.Id);
                customerFromDb.Name = customer.Name;
                customerFromDb.MembershipTypeId = customer.MembershipTypeId;
                customerFromDb.IsSubscribedToNewsLetter = customer.IsSubscribedToNewsLetter;
                customerFromDb.BirthDate = customer.BirthDate;

            }
            movieContext.SaveChanges();
            return RedirectToAction("Index", "Customer");
        }

        public ActionResult Edit(int? id)
        {
            if (!id.HasValue)
            {
                return HttpNotFound();
            }
            var customer = movieContext.Customers.SingleOrDefault(c => c.Id == id.Value);
            if (customer == null)
            {
                return HttpNotFound();
            }
            var membershipTypes = movieContext.MembershipTypes.ToList();
            var customerFormViewModel = new CustomerFormViewModel { MembershipTypes = membershipTypes, Customer = customer };

            return View("CustomerForm", customerFormViewModel);
        }

        public ActionResult Delete(int? id)
        {
            if (!id.HasValue)
            {
                return HttpNotFound();
            }
            var customer = movieContext.Customers.SingleOrDefault(c => c.Id == id.Value);
            if (customer == null)
            {
                return HttpNotFound();
            }
            if (!ModelState.IsValid)
            {
                var membershipTypes = movieContext.MembershipTypes.ToList();
                var customerFormViewModel = new CustomerFormViewModel { MembershipTypes = membershipTypes, Customer = customer };
                return View("CustomerForm", customerFormViewModel);
            }
            customer.IsDeleted = true;
            movieContext.SaveChanges();
            return RedirectToAction("Index", "Customer");
        }
    }
}