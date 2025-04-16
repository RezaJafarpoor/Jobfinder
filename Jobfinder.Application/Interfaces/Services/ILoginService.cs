using Jobfinder.Application.Commons;
using Jobfinder.Application.Dtos.Identity;
using Jobfinder.Application.Interfaces.Common;

namespace Jobfinder.Application.Interfaces.Services;

public interface ILoginService : IScopedService
{
    Task<Response<IdentityResponse>> LoginWithPassword(LoginDto login, CancellationToken cancellationToken);
}