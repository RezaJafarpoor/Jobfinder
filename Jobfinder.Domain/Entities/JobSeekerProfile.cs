namespace Jobfinder.Domain.Entities;

public class JobSeekerProfile 
{
    public Guid Id { get; set; }
    public string Firstname { get; set; } = string.Empty;
    public string Lastname { get; set; } =string.Empty;
    public Cv JobSeekerCv { get; set; } = new();
    public User User { get; set; } = new();
    public Guid UserId { get; set; }
}