namespace DemoApp.Models
{
    public class Detail
    {

        public int DetailId { get; set; }
        public int Amount { get; set; }
        public float Price { get; set; }
        public float SubTotal { get; set; }

        //Products
        public int ProductId { get; set; }
        public Product Product { get; set; }

        //Invoices
        public int InvoiceId { get; set; }
        public Invoice Invoice { get; set; }

    }
}
