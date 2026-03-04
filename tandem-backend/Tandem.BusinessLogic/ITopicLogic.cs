using Tandem.Persistence.Entities;

namespace Tandem.BusinessLogic;

public interface ITopicLogic
{
    public Task<TopicGroup> CreateTopicGroupAsync(string userId, string name);
    public Task<Topic> CreateTopicAsync(Guid topicGroupId, string name);
    public Task DeleteTopicGroupAsync(Guid topicGroupId);
    public Task DeleteTopicAsync(Guid topicGroupId);
}