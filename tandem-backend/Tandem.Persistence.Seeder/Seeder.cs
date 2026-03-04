using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
        var user1 = await GetOrCreateUser("u1", "Foo", "test");
        var user2 = await GetOrCreateUser("u2", "Bar", "test");

        var group1 = await GetOrCreateTopicGroup(CreateGuid(1), user1, "Sports");
        var group2 = await GetOrCreateTopicGroup(CreateGuid(2), user1, "Entertainment");
        await GetOrCreateTopic(CreateGuid(1), group1, "Running", 2);
        await GetOrCreateTopic(CreateGuid(2), group1, "Bouldering", 3);
        await GetOrCreateTopic(CreateGuid(3), group2, "Movie", 3);
        await GetOrCreateTopic(CreateGuid(4), group2, "Game", 4);

        var group3 = await GetOrCreateTopicGroup(CreateGuid(3), user2, "Sports");
        var group4 = await GetOrCreateTopicGroup(CreateGuid(4), user2, "Music");
        await GetOrCreateTopic(CreateGuid(5), group3, "Running", 5);
        await GetOrCreateTopic(CreateGuid(6), group3, "Bouldering", 3);
        await GetOrCreateTopic(CreateGuid(7), group4, "EDM", 3);
        await GetOrCreateTopic(CreateGuid(8), group4, "Rock", 2);
        await GetOrCreateTopic(CreateGuid(9), group4, "LoFi", 1);

        await _context.SaveChangesAsync();
    }

    private async Task<ApplicationUser> GetOrCreateUser(string id, string username, string password)
    {
        var userManager = _serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var logger = _serviceProvider.GetRequiredService<ILogger<Seeder>>();

        var existingUser = await userManager.FindByIdAsync(id);
        if (existingUser is not null)
        {
            return existingUser;
        }

        var newUser = new ApplicationUser
        {
            Id = id,
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

        return newUser;
    }

    private async Task<TopicGroup> GetOrCreateTopicGroup(Guid id, ApplicationUser user, string name)
    {
        var group = await _context.TopicGroups.FirstOrDefaultAsync(g => g.Id == id);
        if (group is not null)
        {
            return group;
        }

        var newGroup = new TopicGroup
        {
            Id = id,
            User = user,
            Name = name
        };

        _context.TopicGroups.Add(newGroup);

        return newGroup;
    }

    private async Task<Topic> GetOrCreateTopic(Guid id, TopicGroup group, string name, int rating)
    {
        var topic = await _context.Topics.FirstOrDefaultAsync(t => t.Id == id);
        if (topic is not null)
        {
            return topic;
        }

        var newTopic = new Topic
        {
            Id = id,
            Name = name,
            Rating = rating,
            TopicGroup = group
        };

        _context.Topics.Add(newTopic);

        return newTopic;
    }

    private static Guid CreateGuid(int value)
    {
        var bytes = new byte[16];
        BitConverter.GetBytes(value).CopyTo(bytes, 0);
        return new Guid(bytes);
    }
}