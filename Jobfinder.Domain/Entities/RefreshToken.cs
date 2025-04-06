namespace Jobfinder.Domain.Entities;

public class RefreshToken
{
    public Guid Id { get; set; }
    public string Token { get; set; } = string.Empty;
    public DateTime ExpirationDate { get; set; }
    public User User { get; set; } = new();
    public Guid UserId { get; set; }

    public RefreshToken() {}

    public RefreshToken(string token, User user)
    {
        Token = token;
        User = user;
        ExpirationDate = DateTime.UtcNow.AddDays(7);
    }

    public bool IsExpired()
        => ExpirationDate < DateTime.UtcNow;


}