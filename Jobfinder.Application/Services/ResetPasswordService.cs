using Jobfinder.Application.Commons;
using Jobfinder.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System.Threading.Channels;

namespace Jobfinder.Application.Services;

public class ResetPasswordService
 (UserManager<User> userManager,
     Channel<EmailContent> channel)
{

    public async Task<Response<string>> ForgetPassword(string email, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByEmailAsync(email);
        if (user is null)
            return Response<string>.Failure("if email is valid, you will receive an email");
        var token = await userManager.GeneratePasswordResetTokenAsync(user);
        var emailContent = new EmailContent("Password Reset", user.Email!, token);
        await channel.Writer.WriteAsync(emailContent, cancellationToken);
        return Response<string>.Success("if email is valid, you will receive an email");
    }


    public async Task<Response<string>> ResetPassword(string email, string password, string token)
    {
        var user = await userManager.FindByEmailAsync(email);
        if (user is null)
            return Response<string>.Failure("Invalid email address");
        var result = await userManager.ResetPasswordAsync(user, token, password);
        return Response<string>.Success("Password changed successfully");
    }
}