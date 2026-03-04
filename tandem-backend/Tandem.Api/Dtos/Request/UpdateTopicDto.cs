using System.ComponentModel.DataAnnotations;

namespace Tandem.Api.Dtos.Request;

public class UpdateTopicDto
{
    [Required]
    public Guid TopicId { get; set; }

    [Required]
    [Range(1, 5)]
    public int Rating { get; set; }
}