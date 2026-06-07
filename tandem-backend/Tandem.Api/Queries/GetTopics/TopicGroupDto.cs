namespace Tandem.Api.Queries.GetTopics;

public class TopicGroupDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public List<TopicDto> Topics { get; set; }
}