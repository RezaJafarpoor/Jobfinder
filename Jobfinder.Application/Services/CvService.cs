using Jobfinder.Application.Commons;
using Jobfinder.Application.Dtos.Cv;
using Jobfinder.Application.Interfaces.Repositories;
using Jobfinder.Domain.Entities;

namespace Jobfinder.Application.Services;

public sealed class CvService (ICvRepository cvRepository)
{
    public async Task<Response<string>> CreateCv(CreateCvDto cvDto, Guid jobSeekerId, CancellationToken cancellationToken)
    {
        var cv = new Cv(cvDto.Location, cvDto.BirthDay, cvDto.MinimumSalary, cvDto.MaximumSalary, cvDto.Status, jobSeekerId);
        await cvRepository.CreateCv(cv);
        if (await cvRepository.SaveChangesAsync(cancellationToken))
            return Response<string>.Success("Cv created");
        return Response<string>.Failure("Something went wrong");
    }
}