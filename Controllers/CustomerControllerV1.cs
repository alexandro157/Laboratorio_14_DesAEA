using DemoApp.Models;
using DemoApp.Models.Request;
using Microsoft.AspNetCore.Mvc;

namespace DemoApp.Controllers
{

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CustomerControllerV1 : Controller
    {

        private readonly InvoiceContext invoiceContext;

        [HttpPost]
        public Customer insertCustomer(CustomerV1 customerData)
        {
            Customer customer = new Customer 
            {
                FirstName = customerData.FirstName,
                LastName = customerData.LastName,
                DocumentNumber = customerData.DocumentNumber,
                Active = 1
            };

            invoiceContext.Customers.Add(customer);
            invoiceContext.SaveChanges();
            return customer;

        }

        [HttpDelete]
        public Customer deleteCustomer(int id)
        {
            Customer customer = invoiceContext.Customers.Where(x => x.CustomerId == id).FirstOrDefault();
            customer.Active = 0;

            invoiceContext.Entry(customer).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            invoiceContext.SaveChanges();

            return customer;

        }

        [HttpPut]
        public Customer updateCustomer(CustomerV2 customerData)
        {
            Customer customer = invoiceContext.Customers.Where(x => x.CustomerId == customerData.Id).FirstOrDefault();
            
            customer.DocumentNumber = customerData.DocumentNumber;

            invoiceContext.Entry(customer).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            invoiceContext.SaveChanges();

            return customer;

        }

    }
}
