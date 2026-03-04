using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tandem.Api.Dtos;
using Tandem.Api.Dtos.Request;
using Tandem.BusinessLogic;

namespace Tandem.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TopicGroupsController : BaseController
{
    private readonly ITopicLogic _topicLogic;

    public TopicGroupsController(ITopicLogic topicLogic)
    {
        _topicLogic = topicLogic;
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Create(CreateTopicGroupDto request)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var topicGroup = await _topicLogic.CreateTopicGroupAsync(userId!, request.Name);

        return Ok(Mapper.Map(topicGroup));
    }
}