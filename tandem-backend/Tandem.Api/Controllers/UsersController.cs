using System.Security.Claims;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Tandem.Api.Dtos;
using Tandem.Api.Queries.GetUsers;
using Tandem.Domain.Users;

namespace Tandem.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly UserManager<ApplicationUser> _userManager;

    public UsersController(IMediator mediator, UserManager<ApplicationUser> userManager)
    {
        _mediator = mediator;
        _userManager = userManager;
    }

    [HttpGet("me")]
    [Authorize]
    public async Task<IActionResult> GetMe()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null)
        {
            return Unauthorized();
        }

        // Retrieve the user from the UserManager
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return NotFound();
        }

        return Ok(Mapper.Map(user));
    }

    [HttpGet("{userId}")]
    [Authorize]
    public async Task<IActionResult> GetUser(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return NotFound();
        }

        return Ok(Mapper.Map(user));
    }

    [HttpGet]
    [Authorize]
    public async Task<List<GetUsersDto>> GetUsers(GetUsersQuery query, CancellationToken ct)
    {
        return await _mediator.Send(query, ct);
    }
}