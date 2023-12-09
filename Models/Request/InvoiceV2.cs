namespace DemoApp.Models.Request
{
    public class InvoiceV2
    {
        public int InvoiceId { get; set; }
        public DateTime Date { get; set; }
        public string InvoiceNumber { get; set; }
        public float Total { get; set; }

    }
}
