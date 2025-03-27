﻿using Jobfinder.Application.Common.Models;
using Jobfinder.Domain.Dtos;
using Jobfinder.Domain.Dtos.Identity;

namespace Jobfinder.Application.Interfaces;

public interface ILoginService
{
    Task<Response<TokenResponse>> Login(LoginDto login, CancellationToken cancellationToken);


}