using Jobfinder.Application.Common.Models;
using Jobfinder.Domain.Dtos;

namespace Jobfinder.Application.Interfaces;

public interface IRegisterService
{
    Task<Response<TokenResponse>> RegisterJobSeekerProfile(RegisterDto register, CancellationToken cancellationToken);
    Task<Response<TokenResponse>> RegisterEmployerProfile(RegisterDto register, CancellationToken cancellationToken);
}