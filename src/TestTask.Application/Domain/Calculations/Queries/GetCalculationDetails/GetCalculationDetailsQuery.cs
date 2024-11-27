using MediatR;

namespace TestTask.Application.Domain.Calculations.Queries.GetCalculationDetails;

public record GetCalculationDetailsQuery(Guid Id) : IRequest<CalculationDetailsDto>;
