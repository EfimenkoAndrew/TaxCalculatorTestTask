using System.Net;
using TestTask.Core.Exceptions;

namespace TestTask.Infrastructure.Exceptions;

public class ExceptionToResponseMapper : IExceptionToResponseMapper
{
    public ExceptionResponse Map(Exception exception)
    {
        return exception switch
        {
            NotFoundException ex => new ExceptionResponse(
                HttpStatusCode.NotFound,
                new
                {
                    ex.Message
                }),
            AlreadyExistsException ex => new ExceptionResponse(
                HttpStatusCode.Conflict,
                new
                {
                    ex.Details,
                    ex.Message
                }),
            ValidationException ex => new ExceptionResponse(
                HttpStatusCode.BadRequest,
                new
                {
                    ex.Message,
                    Errors = ex.Failures
                        .Select(e => new
                        {
                            e.PropertyName,
                            e.ErrorMessage
                        })
                        .ToList()
                }),
            RuleValidationException ex => new ExceptionResponse(
                HttpStatusCode.BadRequest,
                new
                {
                    ex.Message,
                    Errors = ex.Failures
                }),
            // todo: think where to put this const
            _ => new ExceptionResponse(
                HttpStatusCode.InternalServerError,
                new
                {
                    exception.Message,
                    exception.StackTrace
                })
        };
    }
}
