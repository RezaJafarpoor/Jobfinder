using Jobfinder.Domain.Enums;
using static System.String;

namespace Jobfinder.Domain.Entities;

public class Cv
{
    public string City { get; set; } = Empty;
    public string Region { get; set; } = Empty;
    public DateOnly BirthDay { get; set; }
    public int ExpectedSalary { get; set; }
    public MilitaryServiceStatus ServiceStatus { get; set; }
    public JobSeekerProfile JobSeeker { get; set; } = new();
    public Guid JobSeekerId { get; set; }
    public string FullName => $"{JobSeeker.Firstname} {JobSeeker.Lastname}";
}