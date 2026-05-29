namespace WindowsFormsApp1.Models
{
    public class Payment
    {
        public int     Id     { get; set; }
        public int     SaleId { get; set; }
        public decimal Amount { get; set; }
        public string  Status { get; set; } = "paid";
    }
}
