using Library.Api.Common;
using Library.Api.Constants;
using MassTransit.Mediator;
using Microsoft.AspNetCore.Mvc;

namespace TestTask.Api.Domain.Calculations;

[Route(Routes.Calculations)]
public class CalculationsController(IMediator mediator) : ApiControllerBase
{
}
