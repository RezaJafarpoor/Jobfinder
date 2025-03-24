using Jobfinder.Domain.Enums;

namespace Jobfinder.Domain.Entities;

public class JobApplication
{
    public Guid Id { get; set; }
    public JobSeekerProfile JobSeekerProfile { get; set; } = new();
    public Guid JobSeekerProfileId { get; set; }
    public JobOffer JobOffer { get; set; } = new();
    public Guid JobOfferId { get; set; }
    public DateTime AppliedOn { get; set; }
    public JobApplicationStatus Status { get; set; }

}