using Tandem.Persistence.Entities;
using Tandem.Persistence.Interfaces;

namespace Tandem.BusinessLogic.Topics;

public class TopicLogic : ITopicLogic
{
    private const int InitialRating = 3;
    private readonly ITopicRepository _topicRepository;

    public TopicLogic(ITopicRepository topicRepository)
    {
        _topicRepository = topicRepository;
    }

    public async Task<TopicGroup> CreateTopicGroupAsync(string userId, string name)
    {
        var topicGroup = new TopicGroup
        {
            Name = name,
            UserId = userId
        };

        await _topicRepository.CreateAsync(topicGroup);

        return topicGroup;
    }

    public async Task<Topic> CreateTopicAsync(Guid topicGroupId, string name)
    {
        var topic = new Topic
        {
            Name = name,
            Rating = InitialRating,
            TopicGroupId = topicGroupId,
        };

        await _topicRepository.CreateAsync(topic);

        return topic;
    }

    public Task DeleteTopicGroupAsync(Guid topicGroupId)
    {
        return _topicRepository.DeleteTopicGroupAsync(topicGroupId);
    }

    public Task DeleteTopicAsync(Guid id)
    {
        return _topicRepository.DeleteTopicAsync(id);
    }
}