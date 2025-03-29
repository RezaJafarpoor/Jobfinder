using Jobfinder.Domain.Enums;
using Jobfinder.Domain.ValueObjects;

namespace Jobfinder.Domain.Entities;

public class Cv
{
    public Guid Id { get; set; }
    public Location Location { get; set; } = new ();
    public DateOnly? BirthDay { get; set; }
    public int? MinimumExpectedSalary { get; set; }
    public int? MaximumExpectedSalary { get; set; }

    public MilitaryServiceStatus ServiceStatus { get; set; }
    public JobSeekerProfile JobSeeker { get; } = new();
    public Guid JobSeekerId { get; set; }
    public string FullName => $"{JobSeeker.Firstname} {JobSeeker.Lastname}";

    public Cv() {}

    public Cv(Location location, DateOnly? birthDay, int? minimumExpectedSalary, int? maximumExpectedSalary,
        MilitaryServiceStatus? serviceStatus, Guid jobSeekerId)
    {
        Location = location;
        BirthDay = birthDay;
        MinimumExpectedSalary = minimumExpectedSalary;
        MaximumExpectedSalary = maximumExpectedSalary;
        ServiceStatus = serviceStatus ?? MilitaryServiceStatus.NotServedYet;
        JobSeekerId = jobSeekerId;
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