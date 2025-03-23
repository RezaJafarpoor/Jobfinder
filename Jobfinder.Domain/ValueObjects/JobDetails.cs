using Jobfinder.Domain.Enums;

namespace Jobfinder.Domain.ValueObjects;

public class JobDetails
{
    public ContractTypes ContractType { get; set; }
    public WorkingDatsAndHours WorkingDatsAndHours { get; set; } = new();
    public bool IsRemote { get; set; }
    public Location Location { get; set; } = new();
    public Salary Salary { get; set; } = new ();
    public int? MinimumAge { get; set; }
    public int? MaximumAge { get; set; }

}