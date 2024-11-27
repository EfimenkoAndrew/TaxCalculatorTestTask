using Library.Api.Common;
using Library.Api.Constants;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TestTask.Api.Domain.Calculations.Requests.Requests;
using TestTask.Application.Common;
using TestTask.Application.Domain.Calculations.Commands.CreateCalculation;
using TestTask.Application.Domain.Calculations.Queries.GetCalculationDetails;
using TestTask.Application.Domain.Calculations.Queries.GetCalculations;

namespace TestTask.Api.Domain.Calculations;

[Route(Routes.Calculations)]
public class CalculationsController(IMediator mediator) : ApiControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(CreatedResponse<Guid>), StatusCodes.Status201Created)]
    public async Task<IActionResult> Calculate(CreateCalculationRequest request, CancellationToken cancellationToken = default)
    {
        var command = new CreateCalculationCommand(request.GrossAnnualSalary);
        var result = await mediator.Send(command, cancellationToken);
        return CreatedObjectResult(result);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(CalculationDetailsDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCalculation(Guid id, CancellationToken cancellationToken = default)
    {
        var query = new GetCalculationDetailsQuery(id);
        var result = await mediator.Send(query, cancellationToken);
        return Ok(result);
    }

    [HttpGet]
    [ProducesResponseType(typeof(PageResponse<CalculationsDto[]>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCalculations(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        CancellationToken cancellationToken = default)
    {
        var query = new GetCalculationsQuery(pageNumber, pageSize);
        var result = await mediator.Send(query, cancellationToken);
        return Ok(result);
    }
}
