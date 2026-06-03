using WindowsFormsApp1.Database;

namespace WindowsFormsApp1.Services
{
    /// <summary>
    /// Stock business logic - Service Layer
    /// StockControl seedha database se baat nahi karta - Abstraction
    /// </summary>
    public class StockService
    {
        // StockRepository - Encapsulation
        private readonly StockRepository _repo = new StockRepository();

        /// <summary>
        /// Stock add karta hai - Validation check karta hai phir StockRepository se add karta hai
        /// Returns: null = success, string = error message
        /// </summary>
        public string AddStock(int medicineId, int qty)
        {
            // Step 1 - Validation check
            if (qty <= 0)
                return "Quantity 0 se zyada honi chahiye!";
            // Step 2 - Database mein stock add karo
            _repo.AddStock(medicineId, qty);
            return null;  // null = success
        }
    }
}