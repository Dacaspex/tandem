using Microsoft.EntityFrameworkCore;
using Tandem.Persistence.Entities;

namespace Tandem.Persistence.Repositories;

public class UserRepository
{
    private readonly TandemContext _context;

    public UserRepository(TandemContext context)
    {
        _context = context;
    }

    public Task<List<ApplicationUser>> GetUsers()
    {
        return _context.Users.ToListAsync();
    }

    public Task<Topic?> GetTopic(Guid topicId)
    {
        return _context.Topics.FirstOrDefaultAsync(x => x.Id == topicId);
    }

    public Task<List<TopicGroup>> GetTopicGroups(string userId)
    {
        return _context.TopicGroups
            .Include(t => t.Topics)
            .Where(t => t.UserId == userId)
            .ToListAsync();
    }

    public Task SaveChangesAsync()
    {
        return _context.SaveChangesAsync();
    }
}