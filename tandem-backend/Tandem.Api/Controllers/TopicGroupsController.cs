using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tandem.Api.Commands.CreateTopicGroup;
using Tandem.Api.Commands.DeleteTopicGroup;

namespace Tandem.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TopicGroupsController : ControllerBase
{
    private readonly IMediator _mediator;

    public TopicGroupsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Create(CreateTopicGroupCommand command, CancellationToken ct)
    {
        var result = await _mediator.Send(command, ct);

        if (!result.IsSuccess)
        {
            return result.Error switch
            {
                _ => Problem()
            };
        }

        return Ok(result.Value);
    }

    [HttpDelete]
    [Authorize]
    public async Task<IActionResult> Delete(DeleteTopicGroupCommand command, CancellationToken ct)
    {
        var result = await _mediator.Send(command, ct);

        if (!result.IsSuccess)
        {
            return result.Error switch
            {
                DeleteTopicGroupError.TopicGroupNotFound => NotFound(),
                _ => Problem()
            };
        }

        return Ok();
    }
}