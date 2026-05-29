namespace WindowsFormsApp1.Models
{
    public class Medicine
    {
        public int     Id      { get; set; }
        public string  Name    { get; set; } = string.Empty;
        public string  Company { get; set; } = string.Empty;
        public decimal Price   { get; set; }
        public int     Qty     { get; set; }
    }
}
