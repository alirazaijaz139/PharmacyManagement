using WindowsFormsApp1.Database;
using WindowsFormsApp1.Models;
using WindowsFormsApp1.Validators;

namespace WindowsFormsApp1.Services
{
    public class LoginService
    {
        private readonly UserRepository _repo = new UserRepository();
        private readonly LoginValidator _validator = new LoginValidator();

        public string Login(string username, string password, out User user)
        {
            user = null;
            if (!_validator.IsValid(username, password))
                return "Enter Username and Password";

            user = _repo.GetByUsernamePassword(username, password);
            if (user == null)
                return "Wrong Username or Password";

            return null;
        }
    }
}