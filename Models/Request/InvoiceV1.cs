namespace DemoApp.Models.Request
{
    public class InvoiceV1
    {
        public int IdCustomer {  get; set; }
        public DateTime Date { get; set; }
        public string InvoiceNumber { get; set; }
        public float Total { get; set; }
    }
}
