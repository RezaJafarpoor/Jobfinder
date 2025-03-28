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

    public JobSeekerProfile(string firstname, string lastname, User user)
    {
        Firstname = firstname;
        Lastname = lastname;
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