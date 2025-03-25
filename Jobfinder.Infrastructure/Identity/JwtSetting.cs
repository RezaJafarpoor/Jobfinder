namespace Jobfinder.Infrastructure.Identity;

public record JwtSetting
{
    public required string Secret { get; set; } 
    public required string Issuer { get; set; }
    public required int ExpirationTimeInMinute { get; set; }
    
}
