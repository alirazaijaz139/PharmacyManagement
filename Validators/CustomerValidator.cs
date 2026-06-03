using WindowsFormsApp1.Models;

namespace WindowsFormsApp1.Validators
{
    /// <summary>
    /// Customer form validation - Encapsulation
    /// Customer data sahi hai ya nahi check karta hai
    /// </summary>
    public class CustomerValidator
    {
        /// Returns null = valid, string = error message
        public string Validate(Customer c)
        {
            // Name zaroori hai
            if (string.IsNullOrWhiteSpace(c.Name))
                return "Customer name required!";
            // Phone zaroori hai
            if (string.IsNullOrWhiteSpace(c.Phone))
                return "Phone number required!";
            return null;
        }
    }
}