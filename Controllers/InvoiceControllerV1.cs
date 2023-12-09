using DemoApp.Models;
using DemoApp.Models.Request;
using Microsoft.AspNetCore.Mvc;

namespace DemoApp.Controllers
{

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class InvoiceControllerV1 : Controller
    {

        private readonly InvoiceContext invoiceContext;

        [HttpPost]
        public Invoice InsertInvoice(InvoiceV1 invoiceData)
        {

            Invoice invoice = new Invoice
            {
                CustomerId = invoiceData.IdCustomer,
                Date = invoiceData.Date,
                InvoiceNumber = invoiceData.InvoiceNumber,
                Total = invoiceData.Total
            };

            invoiceContext.Invoices.Add(invoice);
            invoiceContext.SaveChanges();
            return invoice;

        }

        [HttpPost]
        public List<Invoice> insertListInvoiceByClient(int idCustomer, List<InvoiceV2> invoices)
        {
            Customer customer = invoiceContext.Customers.Where(x => x.CustomerId == idCustomer).FirstOrDefault();

            List<Invoice> invoiceList = new List<Invoice>();

            foreach (var invoice in invoices)
            {
                Invoice newinvoice = new Invoice();
                newinvoice.InvoiceId = invoice.InvoiceId;
                newinvoice.InvoiceNumber = invoice.InvoiceNumber;
                newinvoice.Total = invoice.Total;
                newinvoice.Date = invoice.Date;
                newinvoice.CustomerId = idCustomer;
                newinvoice.Customer = customer;

                invoiceContext.Invoices.Add(newinvoice);
                invoiceContext.SaveChanges();

                invoiceList.Add(newinvoice);

            }

            return invoiceList;

        }

    }
}
