using Jobfinder.Domain.Dtos.JobOffer;
using Jobfinder.Domain.ValueObjects;

namespace Jobfinder.Domain.Entities;

public class JobOffer
{
   
    public Guid Id { get; set; }
    public string JobName { get; set; } = string.Empty;
    public string JobDescription { get; set; } = string.Empty;
    public JobDetails JobDetails { get; set; } = new();
    public Salary Salary { get; set; } = new ();
    public string CompanyName{ get; set; } = string.Empty;
    public JobCategory Category { get; set; } = new();
    public Guid CategoryId { get; set; }
    public Guid EmployerProfileId { get; set; }

    public List<JobApplication> JobApplications { get; set; } = [];

    public JobOffer() {}

    public JobOffer(string jobName, string jobDescription, JobDetails jobDetails, 
        Salary salary, string companyName, JobCategory jobCategory,
        Guid employerProfileId)
    {
        JobName = jobName;
        JobDescription = jobDescription;
        JobDetails = jobDetails;
        Salary = salary;
        CompanyName = companyName;
        Category = jobCategory;
        EmployerProfileId = employerProfileId;
    }

    public void UpdateJobOfferDto(string jobName, string jobDescription, JobDetails jobDetails,
        Salary salary)
    {
        JobName = jobName;
        JobDescription = jobDescription;
        JobDetails = jobDetails;
        Salary = salary;
    }

    public void AddApplication(JobApplication jobApplication)
    => JobApplications.Add(jobApplication);
    
}