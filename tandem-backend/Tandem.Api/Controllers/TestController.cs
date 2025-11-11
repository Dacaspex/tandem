using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tandem.Api.Dtos;
using Tandem.Persistence;

namespace Tandem.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TestController : BaseController
{
    private readonly TandemContext _context;

    public TestController(TandemContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Test()
    {
        var mapped = Mapper.Map(_context.TopicGroups.Include(t => t.Topics).ToList());
        return Ok(mapped);
    }
}