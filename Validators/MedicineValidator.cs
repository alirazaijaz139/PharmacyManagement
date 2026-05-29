using WindowsFormsApp1.Models;

namespace WindowsFormsApp1.Validators
{
    public class MedicineValidator
    {
        public string Validate(Medicine m)
        {
            if (string.IsNullOrWhiteSpace(m.Name))
                return "Medicine name required!";
            if (string.IsNullOrWhiteSpace(m.Company))
                return "Company name required!";
            if (m.Price <= 0)
                return "Price must be greater than 0!";
            if (m.Qty < 0)
                return "Quantity cannot be negative!";
            return null;
        }
    }
}