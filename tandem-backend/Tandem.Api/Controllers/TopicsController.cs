using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tandem.Api.Commands.CreateTopic;
using Tandem.Api.Commands.DeleteTopic;
using Tandem.Api.Commands.UpdateTopic;
using Tandem.Api.Queries.GetTopics;

namespace Tandem.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TopicsController : ControllerBase
{
    private readonly IMediator _mediator;

    public TopicsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{userId}")]
    [Authorize]
    public async Task<IActionResult> GetTopics(string userId, CancellationToken ct)
    {
        var query = new GetTopicsQuery(userId);
        var result = await _mediator.Send(query, ct);

        if (!result.IsSuccess)
        {
            return result.Error switch
            {
                GetTopicsError.UserNotFoundError => NotFound(),
                _ => Problem()
            };
        }

        return Ok(result.Value);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Create(CreateTopicCommand command, CancellationToken ct)
    {
        var result = await _mediator.Send(command, ct);

        if (!result.IsSuccess)
        {
            return result.Error switch
            {
                CreateTopicError.TopicGroupNotFound => NotFound(),
                _ => Problem()
            };
        }

        return Ok(result.Value);
    }

    [HttpPatch]
    [Authorize]
    public async Task<IActionResult> Update(UpdateTopicCommand command, CancellationToken ct)
    {
        var result = await _mediator.Send(command, ct);

        if (!result.IsSuccess)
        {
            return result.Error switch
            {
                UpdateTopicError.UserNotFound => Unauthorized(),
                UpdateTopicError.TopicNotFound => NotFound(),
                _ => Problem()
            };
        }

        return Ok();
    }

    [HttpDelete]
    [Authorize]
    public async Task<IActionResult> Delete(DeleteTopicCommand command, CancellationToken ct)
    {
        var result = await _mediator.Send(command, ct);

        if (!result.IsSuccess)
        {
            return result.Error switch
            {
                DeleteTopicError.TopicNotFound => NotFound(),
                _ => Problem()
            };
        }

        return Ok();
    }
}