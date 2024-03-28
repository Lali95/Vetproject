namespace Vetproject.Contracts;

public record AuthRequest(
    string Email,
    string Password
);