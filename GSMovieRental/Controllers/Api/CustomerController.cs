using System;

using System.Linq;
using System.Net;

using System.Data.Entity;
using System.Web.Http;
using AutoMapper;
using GSMovieRental.Dto;
using GSMovieRental.Models;

namespace GSMovieRental.Controllers.Api
{
    public class CustomerController : ApiController
    {
        private readonly MovieDbContext movieContext = null;

        public CustomerController()
        {
            movieContext = MovieDbContext.Create();

        }

        public IHttpActionResult GetCustomers(string query = null)
        {
            var customersQuery = movieContext.Customers.Include(c => c.MembershipType);
            if (!String.IsNullOrWhiteSpace(query))
                customersQuery = customersQuery.Where(c => c.Name.Contains(query));

            var customersDto = customersQuery.ToList().Select(Mapper.Map<Customer, CustomerDto>);
            return Ok(customersDto);

        }

        public IHttpActionResult GetCustomer(int? id)

        {
            if (!id.HasValue)
            {
                return BadRequest();
            }
            var customer = movieContext.Customers.SingleOrDefault(c => c.Id == id.Value);
            if (customer == null)
            {
                return NotFound();
            }
            return  Ok(Mapper.Map<Customer,CustomerDto>(customer));

        }
        [HttpPost]
        public IHttpActionResult CreateCustomer(CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var customer = Mapper.Map<CustomerDto, Customer>(customerDto);
            movieContext.Customers.Add(customer);
           
            movieContext.SaveChanges();
            customerDto.Id = customer.Id;
            return Created(new Uri(Request.RequestUri + "/" + customer.Id), customerDto);
        }
        [HttpPut]
        public void UpdateCustomer(CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            var customerFromDb = movieContext.Customers.Single(c => c.Id == customerDto.Id);

            if (customerFromDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            else
            {
                Mapper.Map(customerDto, customerFromDb);
                movieContext.SaveChanges();
            }
           

        }
        [HttpDelete]
        public IHttpActionResult Delete(int? id)
        {
            if (!id.HasValue)
            {
                  return BadRequest(); 
            }
            var customer = movieContext.Customers.SingleOrDefault(c => c.Id == id.Value);
            if (customer == null)
            {
                  return NotFound();
            }
            
            customer.IsDeleted = true;
            movieContext.SaveChanges();
            return Ok();
        }
        protected override void Dispose(bool disposing)
        {
            movieContext.Dispose();
        }
    }
}
