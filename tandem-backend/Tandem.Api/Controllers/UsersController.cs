using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Tandem.Api.Dtos;
using Tandem.Api.Dtos.Request;
using Tandem.Api.Dtos.Response;
using Tandem.Persistence.Entities;
using Tandem.Persistence.Repositories;

namespace Tandem.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : BaseController
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly UserRepository _userRepository;

    public UsersController(UserManager<ApplicationUser> userManager, UserRepository userRepository)
    {
        _userManager = userManager;
        _userRepository = userRepository;
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

    [HttpGet("/{userId}")]
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
    public async Task<List<UserDto>> GetUsers()
    {
        return (await _userRepository.GetUsers()).Select(Mapper.Map).ToList();
    }
}