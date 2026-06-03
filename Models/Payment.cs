namespace WindowsFormsApp1.Models
{
    /// <summary>
    /// Payment Model - Encapsulation
    /// Payment ka data store karta hai
    /// </summary>
    public class Payment
    {
        public int     Id     { get; set; }
        public int     SaleId { get; set; }
        public decimal Amount { get; set; }
        public string  Status { get; set; } = "paid";
    }
}
