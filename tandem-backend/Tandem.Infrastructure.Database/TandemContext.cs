using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Tandem.Domain.Topics;
using Tandem.Domain.Users;

namespace Tandem.Infrastructure.Database;

public class TandemContext : IdentityDbContext<ApplicationUser>
{
    public TandemContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<ApplicationUser> Users { get; set; }
    public DbSet<TopicGroup> TopicGroups { get; set; }
    public DbSet<Topic> Topics { get; set; }
}