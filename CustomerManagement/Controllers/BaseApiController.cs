using CustomerManagement.Core.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace CustomerManagement.Controllers;

[ApiController]
[Produces("application/json")]
public abstract class BaseApiController : ControllerBase
{
    protected IActionResult ToActionResult<T>(Result<T> result) =>
         result.Success ? Ok(result) : BadRequest(result);
}
