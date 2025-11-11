using FluentResults;
using Microsoft.AspNetCore.Mvc;
using Tandem.BusinessLogic.Errors;

namespace Tandem.Api.Controllers;

public abstract class BaseController : ControllerBase
{
    public IActionResult ToActionResult(Result result)
    {
        if (result.IsSuccess) return new OkResult();

        var error = result.Errors.FirstOrDefault();
        return error switch
        {
            NotFoundError => new NotFoundObjectResult(error.Message),
            ValidationError => new BadRequestObjectResult(error.Message),
            _ => new StatusCodeResult(500)
        };
    }
}