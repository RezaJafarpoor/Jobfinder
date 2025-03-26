using Jobfinder.Application.Common.Models;
using Jobfinder.Domain.Dtos;

namespace Jobfinder.Application.Interfaces;

public interface IAuthService
{
    Task<Response<TokenResponse>> RegisterJobSeeker(RegisterDto register, CancellationToken cancellationToken);
}