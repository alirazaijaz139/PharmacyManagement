using WindowsFormsApp1.Database;

namespace WindowsFormsApp1.Services
{
    public class StockService
    {
        private readonly StockRepository _repo = new StockRepository();

        public string AddStock(int medicineId, int qty)
        {
            if (qty <= 0)
                return "Quantity 0 se zyada honi chahiye!";

            _repo.AddStock(medicineId, qty);
            return null;
        }
    }
}