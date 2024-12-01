using MediatR;
using Microsoft.EntityFrameworkCore;
using TestTask.Application.Common;
using TestTask.Application.Domain.Calculations.Queries.GetCalculations;
using TestTask.Persistence.CalculationsDb;

namespace TestTask.Infrastructure.Application.Domain.Calculations.Queries.GetCalculations;

public class GetCalculationsQueryHandler(CalculationsDbContext calculationsDbContext)
    : IRequestHandler<GetCalculationsQuery, PageResponse<CalculationsDto[]>>
{
    public async Task<PageResponse<CalculationsDto[]>> Handle(GetCalculationsQuery query, CancellationToken cancellationToken)
    {
        var sqlQuery = calculationsDbContext.Calculations;
        var total = await sqlQuery.CountAsync(cancellationToken);
        var skip = (query.PageNumber - 1) * query.Count;

        var calculations = await sqlQuery
            .OrderByDescending(x => x.CreatedAt)
            .Skip(skip)
            .Take(query.Count)
            .Select(c => new CalculationsDto(
                c.Id,
                c.GrossAnnualSalary,
                c.GrossMonthlySalary,
                c.NetAnnualSalary,
                c.NetMonthlySalary,
                c.AnnualTaxPaid,
                c.MonthlyTaxPaid))
            .ToArrayAsync(cancellationToken);

        return new PageResponse<CalculationsDto[]>(total, calculations);
    }
}
