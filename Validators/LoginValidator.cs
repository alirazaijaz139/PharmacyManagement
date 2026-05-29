namespace WindowsFormsApp1.Validators
{
    public class LoginValidator
    {
        public bool IsValid(string username, string password)
        {
            return !string.IsNullOrWhiteSpace(username) &&
                   !string.IsNullOrWhiteSpace(password);
        }
    }
}