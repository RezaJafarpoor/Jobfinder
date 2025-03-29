using Jobfinder.Domain.Enums;
using Jobfinder.Domain.ValueObjects;

namespace Jobfinder.Application.Dtos.Cv;

public record CreateCvDto
    (
        Location Location,
        DateOnly BirthDay,
        int? MaximumSalary,
        int? MinimumSalary,
        MilitaryServiceStatus? Status
        );