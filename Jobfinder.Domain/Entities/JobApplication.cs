using Jobfinder.Domain.Enums;
using System.Text.Json.Serialization;

namespace Jobfinder.Domain.Entities;

public class JobApplication
{
    public Guid Id { get; set; }
    [JsonIgnore] public  JobSeekerProfile JobSeekerProfile { get; set; }
    public Guid JobSeekerProfileId { get; set; }
    [JsonIgnore] public JobOffer JobOffer { get; set; } = null!;
    public Guid JobOfferId { get; set; }
    public DateTime AppliedOn { get; set; }
    public JobApplicationStatus Status { get; set; }

    public JobApplication() {}

    public JobApplication(JobSeekerProfile jobSeekerProfile, JobOffer jobOffer)
    {
        JobSeekerProfile = jobSeekerProfile;
        JobOffer = jobOffer;
        AppliedOn = DateTime.UtcNow;
        Status = JobApplicationStatus.Pending;
    }

    public void UpdateJobStatus(JobApplicationStatus status) => Status = status;
}