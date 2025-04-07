using Jobfinder.Application.Commons;

namespace Jobfinder.Application.Interfaces.Common;

public interface IEmailService
{
    Task SendEmailAsync(EmailContent emailContent);
}