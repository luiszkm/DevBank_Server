

using CreateUser.DevBank.Domain.Domain.Exceptions;

namespace User.DevBank.Domain.Domain.Validations;
public class Domainvalidation
{
    public static void NotNull(object? target, string fieldName)
    {
        if (target is null) throw new EntityValidationException($"{fieldName} should not be null");
    }

    public static void NotNullOrEmpty(string? target, string fieldName)
    {
        if (String.IsNullOrWhiteSpace(target))
            throw new EntityValidationException($"{fieldName} should not be empty or null");
    }

    public static void MinLength(string target, int minLength, string fieldName)
    {
        if (target == null || target.Length < minLength)
        {
            throw new ArgumentException($"The field {fieldName} must have at least {minLength} characters.");
        }
    }

    public static void MaxLength(string target, int maxLength, string fieldName)
    {
        if (target.Length > maxLength) throw new EntityValidationException($"{fieldName} should be less or equal {maxLength} characters long");
    }
}
