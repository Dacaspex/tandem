using Tandem.Persistence.Entities;
using Tandem.Persistence.Interfaces;

namespace Tandem.Persistence.Repositories;

public class TopicRepository : ITopicRepository
{
    private readonly TandemContext _context;

    public TopicRepository(TandemContext context)
    {
        _context = context;
    }

    public async Task<TopicGroup> CreateAsync(TopicGroup topicGroup)
    {
        ArgumentNullException.ThrowIfNull(topicGroup);

        topicGroup.Id = Guid.NewGuid();

        await _context.TopicGroups.AddAsync(topicGroup);
        await _context.SaveChangesAsync();

        return topicGroup;
    }

    public async Task<Topic> CreateAsync(Topic topic)
    {
        ArgumentNullException.ThrowIfNull(topic);

        topic.Id = Guid.NewGuid();

        await _context.Topics.AddAsync(topic);
        await _context.SaveChangesAsync();

        return topic;
    }

    public async Task DeleteTopicAsync(Guid id)
    {
        var topic = await _context.FindAsync<Topic>(id);
        if (topic is null) return;
        _context.Topics.Remove(topic);
        await _context.SaveChangesAsync();
    }
}