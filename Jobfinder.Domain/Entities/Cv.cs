using Jobfinder.Domain.Enums;
using Jobfinder.Domain.ValueObjects;
using System.Text.Json.Serialization;

namespace Jobfinder.Domain.Entities;

public class Cv
{
    public Guid Id { get; set; }
    public Location Location { get; set; } = new ();
    public DateOnly? BirthDay { get; set; }
    public int? MinimumExpectedSalary { get; set; }
    public int? MaximumExpectedSalary { get; set; }

    public MilitaryServiceStatus ServiceStatus { get; set; }
    [JsonIgnore] public JobSeekerProfile JobSeeker { get; set; } = null!;
    public Guid JobSeekerId { get; set; }
   

    public Cv() {}

    public Cv(Location location, DateOnly? birthDay, int? minimumExpectedSalary, int? maximumExpectedSalary,
        MilitaryServiceStatus? serviceStatus, JobSeekerProfile profile)
    {
        Location = location;
        BirthDay = birthDay;
        MinimumExpectedSalary = minimumExpectedSalary;
        MaximumExpectedSalary = maximumExpectedSalary;
        ServiceStatus = serviceStatus ?? MilitaryServiceStatus.NotServedYet;
        JobSeekerId = profile.Id;
        JobSeeker = profile;
    }


    public void UpdateCv(Location? location, DateOnly? birthDay, int? minimumExpectedSalary, int? maximumExpectedSalary,
        MilitaryServiceStatus? serviceStatus)
    {
        Location = location ?? Location;
        BirthDay = birthDay ?? BirthDay;
        MinimumExpectedSalary = minimumExpectedSalary ?? MinimumExpectedSalary;
        MaximumExpectedSalary = maximumExpectedSalary ?? MaximumExpectedSalary;
        ServiceStatus = serviceStatus ?? ServiceStatus;
    }
}