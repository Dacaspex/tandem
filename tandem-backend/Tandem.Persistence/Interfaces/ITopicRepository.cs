using Tandem.Persistence.Entities;

namespace Tandem.Persistence.Interfaces;

public interface ITopicRepository
{
    public Task<TopicGroup> CreateAsync(TopicGroup topicGroup);
    public Task<Topic> CreateAsync(Topic topic);
    public Task DeleteTopicAsync(Guid id);
}