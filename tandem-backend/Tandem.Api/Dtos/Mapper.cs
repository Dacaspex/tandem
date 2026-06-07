using Tandem.Api.Queries.GetTopics;
using Tandem.Api.Queries.GetUsers;
using Tandem.Domain.Topics;
using Tandem.Domain.Users;

namespace Tandem.Api.Dtos;

public static class Mapper
{
    public static GetUsersDto Map(ApplicationUser user)
    {
        return new GetUsersDto
        {
            Id = user.Id,
            Name = user.UserName,
        };
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