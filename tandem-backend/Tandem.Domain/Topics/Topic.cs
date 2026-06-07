namespace Tandem.Domain.Topics;

public class Topic
{
    private int _rating;
    private const int InitialRating = 3;

    public Guid Id { get; set; }
    public string Name { get; set; }

    public int Rating
    {
        get => _rating;
        set
        {
            if (_rating == value) return;
            _rating = value;
            LastModifiedAt = DateTime.UtcNow;
        }
    }

    public DateTimeOffset LastModifiedAt { get; set; } = DateTimeOffset.Now;

    public Guid TopicGroupId { get; set; }
    public TopicGroup TopicGroup { get; set; } = null!;

    public static Topic Create(string name, Guid topicGroupId)
    {
        return new Topic
        {
            Id =  Guid.NewGuid(),
            Name = name,
            Rating = InitialRating,
            TopicGroupId = topicGroupId
        };
    }
}