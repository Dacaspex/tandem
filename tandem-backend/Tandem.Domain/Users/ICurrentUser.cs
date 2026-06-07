namespace Tandem.Domain.Users;

public interface ICurrentUser
{
    string UserId { get; }
    bool IsAuthenticated { get; }
}