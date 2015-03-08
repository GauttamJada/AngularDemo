using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using MVCAngularWeb.Models;
using System.Linq.Dynamic;

namespace MVCAngularWeb.Controllers
{
    [RoutePrefix("api/customer")]
    public class CustomerController : ApiController
    {
        private NorthwindEntities db = new NorthwindEntities();

        

        [HttpGet]
        public PagedList GetCustomers(string searchtext, int page = 1, int pageSize = 10, string sortBy = "CustomerID", string sortDirection = "asc")
        {
            var pagedRecord = new PagedList();

            pagedRecord.Content = db.Customers
                        .Where(x => searchtext == null ||
                              (( x.CustomerID.Contains(searchtext)) ||
                                ( x.ContactName.Contains(searchtext)) ||
                                ( x.ContactTitle.Contains(searchtext)) ||
                                ( x.City.Contains(searchtext)) ||
                                ( x.Country.Contains(searchtext))
                            ))
                        .OrderBy(sortBy + " " + sortDirection)
                        .Skip((page -1 ) * pageSize)
                        .Take(pageSize)
                        .ToList();
                
            // Count
            pagedRecord.TotalRecords = db.Customers
                        .Where(x => searchtext == null ||
                              ( (x.CustomerID.Contains(searchtext)) ||
                                (x.ContactName.Contains(searchtext)) ||
                                (x.ContactTitle.Contains(searchtext)) ||
                                (x.City.Contains(searchtext)) ||
                                (x.Country.Contains(searchtext))
                            )).Count();
            
            pagedRecord.CurrentPage = page;
            pagedRecord.PageSize = pageSize;

            return pagedRecord;

        }

        [HttpGet]
        [Route("{id:long}")]
        public Customer GetCustomer(string id)
        {
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return customer;
        }

        [HttpPut]
        [Route("5")]
        public HttpResponseMessage PutCustomer(string id, Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != customer.CustomerID)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(customer).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpPost]
        [Route("")]
        public HttpResponseMessage PostCustomer(Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Customers.Add(customer);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, customer);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = customer.CustomerID }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        [HttpDelete]
        [Route("5")]
        public HttpResponseMessage DeleteCustomer(string id)
        {
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.Customers.Remove(customer);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, customer);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}