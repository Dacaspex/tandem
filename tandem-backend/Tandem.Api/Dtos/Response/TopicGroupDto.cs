namespace Tandem.Api.Dtos.Response;

public class TopicGroupDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public List<TopicDto> Topics { get; set; }
}