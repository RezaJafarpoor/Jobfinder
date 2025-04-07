using Jobfinder.Application.Commons;
using Jobfinder.Application.Interfaces.Common;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

namespace Jobfinder.Infrastructure.Email;

public class EmailService 
    (IOptions<EmailConfig> configuration) : IEmailService
{
    private string Default { get; set; } =@"
<!DOCTYPE html>
<html lang='en'>
<head>
    <meta charset='UTF-8'>
    <title>Password Reset</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            background-color: #f4f6f8;
            margin: 0;
            padding: 0;
        }
        .container {
            max-width: 600px;
            margin: 40px auto;
            background: white;
            padding: 30px;
            border-radius: 10px;
            box-shadow: 0 2px 8px rgba(0,0,0,0.05);
        }
        .button {
            display: inline-block;
            margin: 20px 0;
            background-color: #007bff;
            color: white;
            padding: 12px 24px;
            border-radius: 6px;
            text-decoration: none;
            font-weight: bold;
        }
        .footer {
            margin-top: 30px;
            font-size: 14px;
            color: #999;
            text-align: center;
        }
    </style>
</head>
<body>
    <div class='container'>
        <h2>Reset Your Password</h2>
        <p>Hello,</p>
        <p>We received a request to reset your password. Click the button below to set a new password:</p>
        <p style='text-align: center;'>
            <a href='{RESET_LINK}' class='button'>Reset Password</a>
        </p>
        <p>If you didn't request this, you can safely ignore this email.</p>
        <p>Thanks,<br>The Support Team</p>
        <div class='footer'>
            &copy; 2025 Your Company. All rights reserved.
        </div>
    </div>
</body>
</html>
";
    public async Task SendEmailAsync(EmailContent emailContent)
    {
        try
        { var message = new MimeMessage();
            message.From.Add(new MailboxAddress("JobFinder", configuration.Value.Server));
            message.To.Add(new MailboxAddress("user", emailContent.To));
            var body = Default.Replace("{Reset_Link}", emailContent.Content);
            var bodyBuilder = new BodyBuilder
            {
                HtmlBody = body
            };
            message.Body = bodyBuilder.ToMessageBody();
            using var client = new SmtpClient();
            await client.ConnectAsync(configuration.Value.Server, configuration.Value.Port, SecureSocketOptions.StartTls);
            await client.AuthenticateAsync(configuration.Value.User, configuration.Value.Password);
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
         
    }
}