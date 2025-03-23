using Jobfinder.Domain.Enums;
using Jobfinder.Domain.ValueObjects;
using static System.String;

namespace Jobfinder.Domain.Entities;

public class Cv
{
    public Address Address { get; set; } = new ();
    public DateOnly BirthDay { get; set; }
    public int ExpectedSalary { get; set; }
    public MilitaryServiceStatus ServiceStatus { get; set; }
    public JobSeekerProfile JobSeeker { get; set; } = new();
    public Guid JobSeekerId { get; set; }
    public string FullName => $"{JobSeeker.Firstname} {JobSeeker.Lastname}";
}