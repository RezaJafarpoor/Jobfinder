using Jobfinder.Domain.Commons;
using Jobfinder.Domain.Dtos;
using Jobfinder.Domain.Dtos.Identity;

namespace Jobfinder.Application.Interfaces;

public interface IRegisterService
{
    Task<Response<TokenResponse>> RegisterJobSeekerProfile(RegisterDto register, CancellationToken cancellationToken);
    Task<Response<TokenResponse>> RegisterEmployerProfile(RegisterDto register, CancellationToken cancellationToken);
}