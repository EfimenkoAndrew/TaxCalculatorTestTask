using MediatR;
using TestTask.Application.Domain.Calculations.Queries.GetCalculationDetails;
using TestTask.Persistence.CalculationsDb;

namespace TestTask.Infrastructure.Application.Domain.Calculations.Queries.GetCalculationDetails;

public class GetCalculationDetailsQueryHandler(
    CalculationsDbContext calculationsDbContext)
    : IRequestHandler<GetCalculationDetailsQuery, CalculationDetailsDto>
{
    public async Task<CalculationDetailsDto> Handle(GetCalculationDetailsQuery query, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
