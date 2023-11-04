using System.ComponentModel.DataAnnotations;

namespace BookBazaar.Misc.CustomValidations;

public class CustomRangeAttribute : ValidationAttribute
{
    private readonly int _minimum;
    private readonly int _maximum;
    private readonly string _dependency;

    public CustomRangeAttribute(int minimum, int maximum, string dependency)
    {
        _minimum = minimum;
        _maximum = maximum;
        _dependency = dependency;
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var property = validationContext.ObjectType.GetProperty(_dependency);

        if (property is not null)
        {
            object? dependentValue = property.GetValue(validationContext.ObjectInstance);

            if (value is int intValue && intValue >= _minimum && intValue <= _maximum)
            {
                if (dependentValue is not null && dependentValue is int dependentInt &&
                    intValue <= dependentInt)
                {
                    return ValidationResult.Success;
                }
            }
        }

        return new ValidationResult(ErrorMessage ?? "Invalid value provided");
    }
}