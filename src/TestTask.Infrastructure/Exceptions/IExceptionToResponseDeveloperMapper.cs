namespace TestTask.Infrastructure.Exceptions;

public interface IExceptionToResponseDeveloperMapper
{
    ExceptionResponse Map(Exception exception);
}
