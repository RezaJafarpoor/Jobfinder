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

    public JobSeekerProfile() {}

    public JobSeekerProfile(User user, string? firstname, string? lastname)
    {
        Firstname = firstname ?? string.Empty;
        Lastname = lastname ?? string.Empty;
        User = user;
    }

    public void UpdateProfile(string? firstname, string? lastname)
    {
        Firstname = firstname ?? Firstname;
        Lastname = lastname ?? Lastname;
    }

    public void ApplyToJobOffer(JobApplication jobApplication)
        => JobApplications.Add(jobApplication);
}