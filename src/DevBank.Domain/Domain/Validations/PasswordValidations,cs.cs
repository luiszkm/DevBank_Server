using System.Text.RegularExpressions;

namespace CreateUser.DevBank.Domain.Domain.Validations
{
    public static class PasswordValidations
    {
        public static bool IsValid(string password)
        {
            if (string.IsNullOrWhiteSpace(password) || password.Length < 8)
                return false;

            var hasUpperCase = password.Any(char.IsUpper);
            var hasLowerCase = password.Any(char.IsLower);
            var hasNumber = password.Any(char.IsDigit);
            var hasSpecialChar = Regex.IsMatch(password, @"[!@#$%^&*()_+=\[{\]};:<>|./?,-]");

            return hasUpperCase && hasLowerCase && hasNumber && hasSpecialChar;
        }
    }
}