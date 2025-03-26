using Jobfinder.Application.Common.Models;
using Jobfinder.Domain.Dtos;

namespace Jobfinder.Application.Interfaces;

public interface ILoginService
{
    Task<Response<TokenResponse>> Login(LoginDto login, CancellationToken cancellationToken);


}