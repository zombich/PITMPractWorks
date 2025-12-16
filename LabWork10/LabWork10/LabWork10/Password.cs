namespace LabWork10
{
    public class Password
    {
        public static bool IsValid(string password)
        {
            if (password is null)
                throw new ArgumentNullException();
            return password.Length >= 8 && password.Any(char.IsDigit) && password.Any(char.IsUpper) && password.Any(char.IsLower) && password.Any(c => !char.IsLetterOrDigit(c));
        }
    }
}
