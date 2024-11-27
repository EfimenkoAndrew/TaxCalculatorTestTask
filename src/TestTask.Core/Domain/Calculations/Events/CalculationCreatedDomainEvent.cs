using TestTask.Core.Common;

namespace TestTask.Core.Domain.Calculations.Events;

public record CalculationCreatedDomainEvent(Guid Id) : IDomainEvent;
