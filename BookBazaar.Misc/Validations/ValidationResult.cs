namespace BookBazaar.Misc.Validations;

public class ValidationResult
{
    bool Valid { get; }
    string Message { get; }

    public ValidationResult(bool valid, string message)
    {
        Valid = valid;
        Message = message;
    }
}