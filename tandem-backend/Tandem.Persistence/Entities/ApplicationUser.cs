using Microsoft.AspNetCore.Identity;

namespace Tandem.Persistence.Entities;

public class ApplicationUser : IdentityUser
{
    public string? RefreshToken { get; set; }
    public DateTime RefreshTokenExpiryTime { get; set; }

    public List<TopicGroup> TopicGroups { get; set; }
}