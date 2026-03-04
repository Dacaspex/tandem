namespace Tandem.Api.Dtos.Request;

public class RegisterDto
{
    public required string Username { get; init; }
    public required string Password { get; init; }
}