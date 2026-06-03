using WindowsFormsApp1.Database;
using WindowsFormsApp1.Models;
using WindowsFormsApp1.Validators;

namespace WindowsFormsApp1.Services
{
    /// <summary>
    /// Login business logic - Service Layer
    /// Form directly database se baat nahi karta - Abstraction
    /// </summary>

    public class LoginService
    {
        // Repository aur Validator - Encapsulation
        private readonly UserRepository _repo = new UserRepository();
        private readonly LoginValidator _validator = new LoginValidator();

        /// <summary>
        /// Login karta hai - Validator check karta hai phir Repository se user dhundta hai
        /// Returns: null = success, string = error message
        /// </summary>
        public string Login(string username, string password, out User user)
        {
            user = null;
            // Step 1 - Validation check
            if (!_validator.IsValid(username, password))
                return "Enter Username and Password";
            // Step 2 - Database se user dhundo
            user = _repo.GetByUsernamePassword(username, password);
            // Step 3 - User mila ya nahi
            if (user == null)
                return "Wrong Username or Password";

            return null;   // null = login successful
        }
    }
}