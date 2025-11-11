using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Tandem.Api.Dtos;
using Tandem.Api.Dtos.Request;
using Tandem.BusinessLogic;
using Tandem.Persistence.Entities;
using Tandem.Persistence.Repositories;

namespace Tandem.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TopicsController : BaseController
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly UserRepository _userRepository;
    private readonly UpdateTopicBL _updateTopicBl;

    public TopicsController(UserManager<ApplicationUser> userManager, UserRepository userRepository,
        UpdateTopicBL updateTopicBl)
    {
        _userManager = userManager;
        _userRepository = userRepository;
        _updateTopicBl = updateTopicBl;
    }

    [HttpGet("{userId}")]
    [Authorize]
    public async Task<IActionResult> GetTopics(string userId)
    {
        // Retrieve the user from the UserManager
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return NotFound();
        }

        return Ok(Mapper.Map(await _userRepository.GetTopicGroups(user.Id)));
    }

    [HttpPatch]
    [Authorize]
    public async Task<IActionResult> UpdateTopic(UpdateTopicDto dto)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null)
        {
            return Unauthorized();
        }

        if (dto.Rating is < 1 or > 5)
        {
            return BadRequest("Rating must be between 1 and 5");
        }

        return ToActionResult(await _updateTopicBl.UpdateTopicRating(userId, dto.TopicId, dto.Rating));
    }
}