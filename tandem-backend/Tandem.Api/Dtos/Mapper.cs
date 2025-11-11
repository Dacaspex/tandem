using Tandem.Api.Dtos.Response;
using Tandem.Persistence.Entities;

namespace Tandem.Api.Dtos;

public static class Mapper
{
    public static UserDto Map(ApplicationUser user)
    {
        return new UserDto
        {
            Id = user.Id,
            Name = user.UserName,
        };
    }

    public static IEnumerable<TopicGroupDto> Map(IEnumerable<TopicGroup> topicGroups)
    {
        return topicGroups.Select(Map);
    }

    public static TopicGroupDto Map(TopicGroup topicGroup)
    {
        return new TopicGroupDto
        {
            Id = topicGroup.Id,
            Name = topicGroup.Name,
            Topics = topicGroup.Topics.Select(Map).ToList()
        };
    }

    public static TopicDto Map(Topic topic)
    {
        return new TopicDto
        {
            Id = topic.Id,
            Name = topic.Name,
            Rating = topic.Rating
        };
    }
}