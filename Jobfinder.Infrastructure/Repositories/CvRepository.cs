using Jobfinder.Domain.Dtos.Cv;
using Jobfinder.Domain.Entities;
using Jobfinder.Domain.Enums;
using Jobfinder.Domain.Interfaces;
using Jobfinder.Domain.ValueObjects;
using Jobfinder.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Jobfinder.Infrastructure.Repositories;

//TODO: Add Result pattern to this class

internal class CvRepository
    (ApplicationDbContext dbContext) : ICvRepository
{
    public async Task<bool> CreateCv(Guid jobSeekerId, CreateCvDto createCvDto, CancellationToken cancellationToken)
    {
        var userCv = await dbContext.Cvs.FirstOrDefaultAsync(c => c.JobSeekerId == jobSeekerId, cancellationToken);
        if (userCv is not null) 
            return false;
        var cv = new Cv
        {
            Location = createCvDto.Location ?? new Location(),
            BirthDay = createCvDto.BirthDay ?? null,
            MinimumExpectedSalary = createCvDto.MinimumSalary ?? null,
            MaximumExpectedSalary = createCvDto.MaximumSalary ?? null,
            ServiceStatus = createCvDto.Status ?? MilitaryServiceStatus.NotServedYet,
            JobSeekerId = jobSeekerId
        };

        dbContext.Add(cv);
        return await dbContext.SaveChangesAsync(cancellationToken) > 0;
    }

    public async Task<bool> UpdateCv(Guid jobSeekerId, CreateCvDto cvDto, CancellationToken cancellationToken)
    {
        var userCv = await dbContext.Cvs.FirstOrDefaultAsync(c => c.JobSeekerId == jobSeekerId, cancellationToken);
        if (userCv is null)
            return false;
        userCv.Location = cvDto.Location ?? userCv.Location;
        userCv.BirthDay = cvDto.BirthDay ?? userCv.BirthDay;
        userCv.ServiceStatus = cvDto.Status ?? userCv.ServiceStatus;
        userCv.MaximumExpectedSalary = cvDto.MaximumSalary ?? userCv.MaximumExpectedSalary;
        userCv.MinimumExpectedSalary = cvDto.MinimumSalary ?? userCv.MinimumExpectedSalary;

        return await dbContext.SaveChangesAsync(cancellationToken) > 0;
    }

    public async Task<List<Cv>?> GetCvs(CancellationToken cancellationToken)
    {
        var cvs = await dbContext.Cvs.ToListAsync(cancellationToken);
        return cvs;
    }

    public async Task<Cv?> GetCvByUserId(Guid jobSeekerId, CancellationToken cancellationToken)
    {
        var cv = await dbContext.Cvs.FirstOrDefaultAsync(c => c.JobSeekerId == jobSeekerId, cancellationToken);
        return cv;
    }
}