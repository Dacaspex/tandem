namespace Tandem.Persistence.Entities;

public class TopicGroup
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    public string UserId { get; set; }
    public ApplicationUser User { get; set; }
    public ICollection<Topic> Topics { get; set; } =  new List<Topic>();
}