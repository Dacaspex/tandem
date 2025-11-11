namespace Tandem.Api.Dtos.Request;

public class UpdateTopicDto
{
    public Guid TopicId { get; set; }
    public int Rating { get; set; }
}