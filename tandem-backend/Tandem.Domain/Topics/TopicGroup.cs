using Tandem.Domain.Users;

namespace Tandem.Domain.Topics;

public class TopicGroup
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string UserId { get; set; }

    public ApplicationUser User { get; set; }
    public ICollection<Topic> Topics { get; set; } =  new List<Topic>();

    public static TopicGroup Create(string name, string userId)
    {
        return new TopicGroup
        {
            Id = Guid.NewGuid(),
            Name = name,
            UserId = userId,
        };
    }
}