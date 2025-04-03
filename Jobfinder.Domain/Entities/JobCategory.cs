namespace Jobfinder.Domain.Entities;

public class JobCategory
{
    public Guid Id { get; set; }
    public string Category { get; set; } = string.Empty;
    public List<JobOffer> JobOffers { get; set; } = [];

    public JobCategory() {}
    public JobCategory(string category)
    {
        Category = category;
    }

    public void UpdateCategory(string category)
    {
        Category = category;
    }
}