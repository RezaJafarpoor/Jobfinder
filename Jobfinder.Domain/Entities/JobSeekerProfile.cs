namespace Jobfinder.Domain.Entities;

public class JobSeekerProfile 
{
    public Guid Id { get; set; }
    public string Firstname { get; set; } = string.Empty;
    public string Lastname { get; set; } = string.Empty;
    public Cv? JobSeekerCv { get; set;}
    public Guid? CvId { get; set; }
    public User User { get; set; } = new();
    public List<JobApplication> JobApplications { get; set; } = [];
    public Guid UserId { get; set; }
}