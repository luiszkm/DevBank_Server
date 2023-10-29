

namespace User.DevBank.Application.UseCases.Exceptions;
public class UseCaseValidationException : Exception
{
    public UseCaseValidationException(string? message) : base(message)
    {
    }
}
