using MediatR;
using TestTask.Application.Common;

namespace TestTask.Application.Domain.Calculations.Queries.GetCalculations;

public record GetCalculationsQuery(int PageNumber, int Count) : IRequest<PageResponse<CalculationsDto[]>>;
