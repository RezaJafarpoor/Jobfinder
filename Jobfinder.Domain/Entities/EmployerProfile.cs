using System.Text.Json.Serialization;

namespace Jobfinder.Domain.Entities;

public class EmployerProfile 
{
    public Guid Id { get; set; }
    [JsonIgnore]
    public Company? Company { get; set; } 
    public List<JobOffer> JobOffers { get; set; } = [];
    public User User { get; set; } = new();
    public Guid UserId { get; set; }

    public EmployerProfile() {}

    public EmployerProfile(User user)
    {
        User = user;
    }

    public void CreateJobOffer(JobOffer jobOffer)
    {
        JobOffers.Add(jobOffer);
    }

    public void AddCompany(Company company)
    {
        Company = company;
    }
}