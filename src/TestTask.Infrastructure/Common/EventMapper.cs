using TestTask.Core.Common;
using TestTask.Core.Domain.Calculations.Events;
using TestTask.Infrastructure.SystemEvents;

namespace Library.Infrastructure.Common;

internal static class EventMapper
{
    public static IEnumerable<object?> MapAll(IEnumerable<IDomainEvent> events)
    {
        return events.Select(Map);
    }

    public static object? Map(IDomainEvent @event)
    {
        return @event switch
        {
            CalculationCreatedDomainEvent e => new CalculationCreated(e.Id),
            _ => null
        };
    }
}
