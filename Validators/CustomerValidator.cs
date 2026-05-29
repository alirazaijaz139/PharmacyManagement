using WindowsFormsApp1.Models;

namespace WindowsFormsApp1.Validators
{
    public class CustomerValidator
    {
        public string Validate(Customer c)
        {
            if (string.IsNullOrWhiteSpace(c.Name))
                return "Customer name required!";
            if (string.IsNullOrWhiteSpace(c.Phone))
                return "Phone number required!";
            return null;
        }
    }
}