using WindowsFormsApp1.Models;

namespace WindowsFormsApp1.Validators
{
    /// <summary>
    /// Medicine form validation - Encapsulation
    /// Medicine data sahi hai ya nahi check karta hai
    /// </summary>
    public class MedicineValidator
    {
        /// Returns: null = valid, string = error message
        public string Validate(Medicine m)
        {
            // Name zaroori hai
            if (string.IsNullOrWhiteSpace(m.Name))
                return "Medicine name required!";
            // Company zaroori hai
            if (string.IsNullOrWhiteSpace(m.Company))
                return "Company name required!";
            // Price 0 se zyada honi chahiye
            if (m.Price <= 0)
                return "Price must be greater than 0!";
            // Quantity negative nahi honi chahiye
            if (m.Qty < 0)
                return "Quantity cannot be negative!";
            return null;
        }
    }
}