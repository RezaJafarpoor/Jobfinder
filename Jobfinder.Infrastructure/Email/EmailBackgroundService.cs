using Jobfinder.Application.Commons;
using Jobfinder.Application.Interfaces.Common;
using Microsoft.Extensions.Hosting;
using System.Threading.Channels;

namespace Jobfinder.Infrastructure.Email;

public class EmailBackgroundService
    (Channel<EmailContent> channel,
        IEmailService emailService)
    : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (await channel.Reader.WaitToReadAsync(stoppingToken))
        {
            var email = await channel.Reader.ReadAsync(stoppingToken);
            await emailService.SendEmailAsync(email);
        }
    }
}