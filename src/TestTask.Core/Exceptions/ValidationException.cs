using FluentValidation.Results;

namespace TestTask.Core.Exceptions;

public class ValidationException : DomainException
{
    // todo: think how to change ValidationFailure and do we need to do it
    public ValidationException(List<ValidationFailure> failures) : base("Validation is failed.")
    {
        Failures = failures.AsReadOnly();
    }

    // todo: think how to change ValidationFailure and do we need to do it
    public ValidationException(ValidationFailure failure) : base("Validation is failed.")
    {
        Failures = new[]
        {
            failure
        };
    }

    public IReadOnlyCollection<ValidationFailure> Failures { get; }
}
