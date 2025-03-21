using static System.String;

namespace Jobfinder.Domain.Entities;

public class JobSeekerProfile
{
    public Guid Id { get; set; }
    public string Firstname { get; set; } = Empty;
    public string Lastname { get; set; } = Empty;
    public User User { get; set; } = new();
    public Guid UserId { get; set; }
}