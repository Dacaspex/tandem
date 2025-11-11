namespace Tandem.Persistence.Entities;

public class Topic
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Rating { get; set; }

    public Guid TopicGroupId { get; set; }
    public TopicGroup TopicGroup { get; set; } = null!;
}