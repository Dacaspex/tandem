using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Tandem.Persistence.Entities;

namespace Tandem.Persistence.Seeder;

public class Seeder
{
    private readonly IServiceProvider _serviceProvider;
    private readonly TandemContext _context;

    public Seeder(IServiceProvider serviceProvider, TandemContext context)
    {
        _serviceProvider = serviceProvider;
        _context = context;
    }

    public async Task Seed()
    {
        await CreateUser("Casper", "test");
        await CreateUser("Tom", "test");

        await CreateTopics("Casper", new List<TopicGroup>
        {
            new()
            {
                Name = "Sports",
                Topics = new List<Topic>
                {
                    new()
                    {
                        Name = "Running"
                    }
                }
            }
        });
    }

    private async Task CreateUser(string username, string password)
    {
        var userManager = _serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var logger = _serviceProvider.GetRequiredService<ILogger<Seeder>>();

        var newUser = new ApplicationUser
        {
            UserName = username,
            Email = $"{username}@email.com",
            EmailConfirmed = true
        };

        var result = await userManager.CreateAsync(newUser, password);
        if (!result.Succeeded)
        {
            logger.LogError(
                "Failed to create user: {errors}",
                string.Join(", ", result.Errors.Select(e => e.Description)));
        }
    }

    private async Task CreateTopics(string username, List<TopicGroup> topicGroups)
    {
        var user = _context.Users.FirstOrDefault(u => u.UserName == username);
        if (user == null)
        {
            throw new InvalidOperationException();
        }

        foreach (var group in topicGroups)
        {
            group.User = user;
            // foreach (var topic in group.Topics)
            // {
            //     topic.TopicGroup = group;
            // }

            // await _context.Topics.AddRangeAsync(group.Topics);
        }

        await _context.TopicGroups.AddRangeAsync(topicGroups);
        await _context.SaveChangesAsync();
    }
}