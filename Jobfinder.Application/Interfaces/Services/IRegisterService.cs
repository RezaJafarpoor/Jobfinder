using Jobfinder.Application.Commons;
using Jobfinder.Application.Dtos.Identity;
using Jobfinder.Application.Interfaces.Common;

namespace Jobfinder.Application.Interfaces.Services;

public interface IRegisterService: IScopedService
{
    Task<Response<IdentityResponse>> Register(RegisterDto register, CancellationToken cancellationToken);
}