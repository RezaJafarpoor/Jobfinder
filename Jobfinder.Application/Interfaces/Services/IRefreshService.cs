using Jobfinder.Application.Commons;
using Jobfinder.Application.Dtos.Identity;
using Jobfinder.Application.Interfaces.Common;

namespace Jobfinder.Application.Interfaces.Services;

public interface IRefreshService: IScopedService
{
    Task<Response<IdentityResponse>> CheckRefreshToken(string oldToken, CancellationToken cancellationToken);
}