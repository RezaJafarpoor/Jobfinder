namespace Jobfinder.Infrastructure.Email;

public record EmailConfig
    (string Server, int Port, string User, string Password);