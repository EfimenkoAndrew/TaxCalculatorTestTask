using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Common;

[ApiController]
public abstract class ApiControllerBase : ControllerBase
{
    protected ObjectResult CreatedObjectResult<T>(T createdObject)
    {
        return StatusCode(StatusCodes.Status201Created, createdObject);
    }

    protected ObjectResult Created<T>(T id)
    {
        return StatusCode(StatusCodes.Status201Created, new CreatedResponse<T> { Id = id });
    }

    protected ObjectResult Created<T>(IReadOnlyCollection<T> ids)
    {
        return StatusCode(StatusCodes.Status201Created, ids.Select(id => new CreatedResponse<T> { Id = id }));
    }
}
