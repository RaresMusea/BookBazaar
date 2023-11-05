namespace BookBazaar.Misc.Validations;

public class ValidationResult
{
    public bool Valid { get; private set; }
    public string Message { get; private set; }

    public ValidationResult(bool valid, string message)
    {
        Valid = valid;
        Message = message;
    }
}