﻿using Jobfinder.Application.Interfaces.Repositories;
using Jobfinder.Domain.Entities;
using Jobfinder.Infrastructure.Persistence;

namespace Jobfinder.Infrastructure.Repositories;

internal class RefreshTokenRepository
    (ApplicationDbContext dbContext) : IRefreshTokenRepository
{
    public async Task<bool> AddTokenForUser(RefreshToken token)
    {
        dbContext.Add(token);
        return await dbContext.SaveChangesAsync() > 0;
    }
}