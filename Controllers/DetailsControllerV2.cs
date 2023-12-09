using DemoApp.Models;
using DemoApp.Models.Request;
using Microsoft.AspNetCore.Mvc;

namespace DemoApp.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DetailsControllerV2 : Controller
    {

        private readonly InvoiceContext invoiceContext;

        [HttpPost]
        public List<Detail> insertDetailsListInvoice(int idInvoice, List<DetailV1> details)
        {
            Invoice invoice = invoiceContext.Invoices.Where(x => x.InvoiceId == idInvoice).FirstOrDefault();

            List<Detail> detailsList = new List<Detail>();

            foreach (var detail in details)
            {
                Detail newDetails = new Detail();
                newDetails.InvoiceId = idInvoice;
                newDetails.Invoice = invoice;
                newDetails.Price = detail.Price;
                newDetails.Amount = detail.Amount;
                newDetails.SubTotal = detail.SubTotal;

                invoiceContext.Details.Add(newDetails);
                invoiceContext.SaveChanges();

                detailsList.Add(newDetails);

            }

            return detailsList;
        }

    }
}
