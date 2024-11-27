using MediatR;
using Microsoft.EntityFrameworkCore;
using TestTask.Application.Domain.Calculations.Queries.GetCalculationDetails;
using TestTask.Core.Exceptions;
using TestTask.Persistence.CalculationsDb;

namespace TestTask.Infrastructure.Application.Domain.Calculations.Queries.GetCalculationDetails;

public class GetCalculationDetailsQueryHandler(
    CalculationsDbContext calculationsDbContext)
    : IRequestHandler<GetCalculationDetailsQuery, CalculationDetailsDto>
{
    public async Task<CalculationDetailsDto> Handle(GetCalculationDetailsQuery query, CancellationToken cancellationToken)
    {
        return await calculationsDbContext
            .Calculations
            .Where(x => x.Id == query.Id)
            .Select(x => new CalculationDetailsDto(
                x.Id,
                x.GrossAnnualSalary,
                x.GrossMonthlySalary,
                x.NetAnnualSalary,
                x.NetMonthlySalary,
                x.AnnualTaxPaid,
                x.MonthlyTaxPaid))
            .FirstOrDefaultAsync(cancellationToken)
            ?? throw new NotFoundException($"{nameof(Task)} with id: '{query.Id}' was not found.");
    }
}
