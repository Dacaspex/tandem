using System.ComponentModel.DataAnnotations;

namespace Tandem.Api.Dtos.Request;

public class CreateTopicDto
{
    [Required]
    public Guid TopicGroupId { get; init; }

    [Required]
    [MinLength(1)]
    public required string Name { get; init; }
}