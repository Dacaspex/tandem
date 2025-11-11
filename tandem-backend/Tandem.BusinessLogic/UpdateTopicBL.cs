using FluentResults;
using Microsoft.AspNetCore.Identity;
using Tandem.BusinessLogic.Errors;
using Tandem.Persistence.Entities;
using Tandem.Persistence.Repositories;

namespace Tandem.BusinessLogic;

public class UpdateTopicBL
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly UserRepository _userRepository;

    public UpdateTopicBL(UserManager<ApplicationUser> userManager, UserRepository userRepository)
    {
        _userManager = userManager;
        _userRepository = userRepository;
    }

    public async Task<Result> UpdateTopicRating(string userId, Guid topicId, int newRating)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user is null)
        {
            return Result.Fail(new NotFoundError("User not found"));
        }

        var topic = await _userRepository.GetTopic(topicId);
        if (topic is null)
        {
            return Result.Fail(new NotFoundError("Topic not found"));
        }

        topic.Rating = newRating;

        await _userRepository.SaveChangesAsync();

        return Result.Ok();
    }
}