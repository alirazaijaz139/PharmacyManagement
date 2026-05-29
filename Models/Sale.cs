using System;
namespace WindowsFormsApp1.Models
{
    public class Sale
    {
        public int      Id         { get; set; }
        public int      CustomerId { get; set; }
        public decimal  Total      { get; set; }
        public DateTime SaleDate   { get; set; } = DateTime.Now;
    }

    public class SaleItem
    {
        public int     Id         { get; set; }
        public int     SaleId     { get; set; }
        public int     MedicineId { get; set; }
        public decimal Price      { get; set; }
        public int     Qty        { get; set; }
        public decimal Subtotal   { get; set; }
    }
}
