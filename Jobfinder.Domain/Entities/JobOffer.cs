using Jobfinder.Domain.ValueObjects;

namespace Jobfinder.Domain.Entities;

public class JobOffer
{
    public Guid Id { get; set; }
    public string JobName { get; set; } = string.Empty;
    public string JobDescription { get; set; } = string.Empty;
    public JobDetails JobDetails { get; set; } = new();
    public string CompanyName{ get; set; } = string.Empty;
    public JobCategory Category { get; set; } = new();
    public Guid CategoryId { get; set; }


}