namespace Jobfinder.Infrastructure.Email;

public class EmailConfig
{
    public required string Server { get; set; } 
    public required int Port { get; set;} 
    public required string User { get; set;} 
    public required string Password { get; set;} 
}