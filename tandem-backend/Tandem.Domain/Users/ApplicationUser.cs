using Microsoft.AspNetCore.Identity;
using Tandem.Domain.Topics;

namespace Tandem.Domain.Users;

public class ApplicationUser : IdentityUser
{
    public string? RefreshToken { get; set; }
    public DateTime RefreshTokenExpiryTime { get; set; }

    public List<TopicGroup> TopicGroups { get; set; }
}