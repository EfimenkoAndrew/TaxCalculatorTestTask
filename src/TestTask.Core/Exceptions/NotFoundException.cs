namespace TestTask.Core.Exceptions;

public class NotFoundException(string message) : DomainException(message);
