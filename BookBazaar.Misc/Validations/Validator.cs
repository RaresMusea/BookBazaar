using System.Text.RegularExpressions;

namespace BookBazaar.Misc.Validations;

public static class Validator
{
    public static ValidationResult ValidateName(string name)
    {
        var tokens = name.Split(" ");

        if (tokens.Length < 2)
        {
            return new ValidationResult(false, "The name should contain both your first and your last name!");
        }

        foreach (string token in tokens)
        {
            if (token.Length < 3)
            {
                return new ValidationResult(false, $"Invalid token '{token}' as a part of your name!");
            }
        }

        return new ValidationResult(true, string.Empty);
    }

    public static ValidationResult ValidatePhoneNumber(string phoneNumber)
    {
        string pattern = @"^\(?\d{3}\)?[-.\s]?\d{3}[-.\s]?\d{4}$";

        if (!Regex.IsMatch(phoneNumber, pattern))
        {
            return new ValidationResult(false, "Invalid phone number!");
        }

        return new ValidationResult(true, string.Empty);
    }
}