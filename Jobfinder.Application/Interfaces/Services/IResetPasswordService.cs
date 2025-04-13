using Jobfinder.Application.Commons;
using Jobfinder.Application.Interfaces.Common;

namespace Jobfinder.Application.Interfaces.Services;

public interface IResetPasswordService: IScopedService
{
    Task<Response<string>> ForgetPassword(string email, CancellationToken cancellationToken);
    Task<Response<string>> ResetPassword(string email, string password, string token);
}